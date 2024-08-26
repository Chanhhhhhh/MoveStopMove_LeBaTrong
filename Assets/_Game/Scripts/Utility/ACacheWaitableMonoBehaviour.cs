using System.Threading;


    public class ACacheWaitableMonoBehaviour : ACacheMonoBehaviour
    {
        protected CancellationTokenSource OnDestroyCancellationTokenSource;

        public CancellationToken OnDestroyCancellationToken
        {
            get
            {
                OnDestroyCancellationTokenSource ??= new CancellationTokenSource();
                return OnDestroyCancellationTokenSource.Token;
            }
        }

        protected virtual void OnDestroy()
        {
            OnDestroyCancellationTokenSource?.Cancel();
            OnDestroyCancellationTokenSource?.Dispose();
        }
    }