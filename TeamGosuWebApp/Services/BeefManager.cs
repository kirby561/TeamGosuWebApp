using Microsoft.AspNetCore.SignalR;
using System.Threading;
using TeamGosuWebApp.Utility;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;

namespace TeamGosuWebApp.Services {
    public class BeefManager : IHostedService, IDisposable {
        private readonly int TimeoutCode = -9001;
        private readonly ILogger<BeefManager> _logger;
        private IHubContext<BeefHub> _hubContext;
        private int _eventPort = 5002;
        private String _eventIp = "127.0.0.1";

        private bool _shouldRun = true;
        private bool _isRunning = false;
        private object _lock = new object();
        private Thread _backgroundThread;

        public BeefManager(ILogger<BeefManager> logger, IHubContext<BeefHub> hubContext) {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken stoppingToken) {
            _backgroundThread = new Thread(ServiceThread);
            _backgroundThread.Start();
            await Task.Run(() => {
                // Wait for the thread to start.
                lock (_lock) {
                    while (!_isRunning) {
                        Monitor.Wait(_lock);
                    }
                }
            });
        }

        public async Task StopAsync(CancellationToken stoppingToken) {
            lock (_lock) {
                _shouldRun = false;
            }
            await Task.Run(() => {
                // Wait for the thread to stop.
                lock (_lock) {
                    while (_isRunning) {
                        Monitor.Wait(_lock);
                    }
                }
            });
        }

        public void Dispose() {
            // Nothing to do
        }

        private void ServiceThread() {
            // Notify that the service has started
            lock (_lock) {
                _isRunning = true;
                Monitor.PulseAll(_lock);
            }

            IPAddress beefServerIp = IPAddress.Parse(_eventIp);
            Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connection.ReceiveTimeout = 500;

            while (true) {
                // Make sure we're connected to a server
                while (!connection.Connected && _shouldRun) {
                    try {
                        connection.Connect(beefServerIp, _eventPort);
                    } catch (SocketException) { 
                        // Don't care, try again
                    } catch (Exception ex) {
                        _logger.LogError("Exception trying to connect to the beef ladder server: " + ex.Message);
                        _shouldRun = false;
                        break;
                    } 
                }

                // Get the length
                int result = 0;
                byte[] lengthBytes = new byte[4];
                if (ReadAll(connection, lengthBytes) < 0) {
                    _logger.LogWarning("Lost connection to the ladder update event notifier. Result: " + result);
                } else {
                    // Read the message of length bytes
                    int length = ReadIntNetworkOrder(lengthBytes);
                    byte[] messageBytes = new byte[length];
                    if (ReadAll(connection, messageBytes) < 0) {
                        _logger.LogWarning("Lost connection to the ladder update event notifier after retrieving length. Result: " + result);
                    } else {
                        // The joke is we don't care what the message is since we only support OnLadderChanged right now
                        Task.Run(async () => await OnLadderChanged());
                    }
                }

                // Check if we're quitting
                lock (_lock) {
                    if (!_shouldRun)
                        break;
                }
            }

            // Notify that the service has ended
            lock (_lock) {
                _isRunning = false;
                Monitor.PulseAll(_lock);
            }
        }
        
        private async Task OnLadderChanged() {
            await _hubContext.Clients.All.SendAsync("OnBeefLadderUpdated");
        }

        /// <summary>
        /// Reads the given bytes as a 4 byte integer in Big Endian byte order.
        /// </summary>
        /// <param name="bytes">The integer bytes to read.</param>
        /// <returns>The integer.</returns>
        private int ReadIntNetworkOrder(byte[] bytes) {
            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads from the given socket until byteBuffer is full, an error occurs, or the service is cancelled.
        /// </summary>
        /// <param name="connection">The socket to read from.</param>
        /// <param name="byteBuffer">The byte buffer to read into and the number of bytes to read (defined by the size of the buffer).</param>
        /// <returns>Returns the number of bytes read or a negative number indicating the socket error.</returns>
        private int ReadAll(Socket connection, byte[] byteBuffer) {
            int index = 0;
            int result = 0;
            int bytesRead = 0;
            while (bytesRead < byteBuffer.Length) {
                result = 0;
                try {
                    result = connection.Receive(byteBuffer, index, byteBuffer.Length - index, SocketFlags.None);
                } catch (SocketException) {
                    // Timeout. This is fine
                }
                if (result < 0) {
                    break; // Error
                } else if (result == 0) {
                    // Check if we're supposed to be stopping, 
                    // otherwise just keep listening for messages.
                    lock (_lock) {
                        if (_shouldRun) 
                            continue;
                        else
                            return TimeoutCode;
                    }
                } else {
                    bytesRead += result;
                }
            }

            if (result <= 0)
                return result;
            return bytesRead;
        }
    }
}
