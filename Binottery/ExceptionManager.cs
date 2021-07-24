using Binottery.Log;
using System;
using System.IO;

namespace Binottery
{
    public sealed class ExceptionManager
    {
        private static readonly Lazy<ExceptionManager> lazy = new Lazy<ExceptionManager>(() => new ExceptionManager());

        /// <summary>
        /// OnError event, will be raised when calling to RaiseException method
        /// </summary>
        public event ErrorEventHandler OnError;

        private bool HasError = false;

        private ExceptionManager()
        {
        }

        public static ExceptionManager Instance { get { return lazy.Value; } }

        /// <summary>
        /// Raise an exception to the 'OnError' subscribers if there is no older error
        /// </summary>
        /// <param name="e">exception</param>
        public void RaiseException(Exception e)
        {
            if(!HasError)
            {
                LogManager.Log.Error(e);

                HasError = true;
                if(OnError != null)
                {
                    OnError(this, new ErrorEventArgs(e));
                }
            }
        }
    }
}
