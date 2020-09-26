namespace MegaScrypt
{
    public class InvocationContext
    {
        private Object _container;
        public Object Container => _container;

        public InvocationContext(Object container)
        {
            _container = container;
        }
    }
}
