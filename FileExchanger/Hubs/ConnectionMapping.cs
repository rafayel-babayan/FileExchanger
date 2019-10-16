using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.Hubs
{
    public class ConnectionMapping<T>
    {
        /// <summary>
        /// Connection container
        /// </summary>
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();
        /// <summary>
        ///  Returns key's connections 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Count(T key) => _connections[key]?.Count ?? 0;
        /// <summary>
        /// Returns key's is connected
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasConnected(T key)
        {
            lock (_connections)
            {
                HashSet<string> connections;

                if (!_connections.TryGetValue(key, out connections))
                    return false;
                return connections.Count > 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionId"></param>
        public void Add(T key, string connectionId)
        {
            HashSet<string> connections;
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        /// <summary>
        /// Returns key's all connections
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }
        /// <summary>
        /// Remove concrete connection
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionId"></param>
        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;

                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (!HasConnected(key))
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
