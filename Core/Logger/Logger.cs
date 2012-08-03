using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Logger
{
    public class Logger
    {
        Dictionary<string, ILog> logengines;
        protected Logger()
        {
            logengines = new Dictionary<string, ILog>();
        }
        static Logger _instance;
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();

                }
                return _instance;
            }
        }

        public static void AddLogEngine(string engineName, ILog engine)
        {
            if (!Instance.logengines.ContainsKey(engineName))
            {
                Instance.logengines.Add(engineName, engine);
            }
        }

        public static void RemoveLogEngine(string engineName)
        {
            if (!Instance.logengines.ContainsKey(engineName))
            {
                Instance.logengines.Remove(engineName);
            }
        }

        #region Static ILog

        public static void Debug(object message)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Debug(message);
            }
        }

        public static void Debug(object message, Exception exception)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Debug(message, exception);
            }
        }

        public static void Info(object message)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Info(message);
            }
        }

        public static void Info(object message, Exception exception)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Info(message, exception);
            }
        }

        public static void Warn(object message)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Warn(message);
            }
        }

        public static void Warn(object message, Exception exception)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Warn(message, exception);
            }
        }

        public static void Error(object message)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Error(message);
            }
        }

        public static void Error(object message, Exception exception)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Error(message, exception);
            }
        }

        public static void Fatal(object message)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Fatal(message);
            }
        }

        public static void Fatal(object message, Exception exception)
        {
            foreach (var c in Instance.logengines.Values)
            {
                c.Fatal(message, exception);
            }
        }
        #endregion
    }
}
