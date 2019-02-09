using System;
using System.IO;
using System.Threading.Tasks;

namespace Generazor
{
    public abstract class GenerazorPage<TModel> : IActivatePage<TModel>
    {
        private int         _position;
        private int         _length;
        private bool        _isLiteral;
        private TextWriter  _textWriter;

        void IActivatePage<TModel>.Activate(TextWriter textWriter, TModel model)
        {
            _textWriter = textWriter;
            Model = model;
        }

        protected TModel Model { get; set; }

        protected void BeginContext(int position, int length, bool isLiteral)
        {
            _position = position;
            _length = length;
            _isLiteral = isLiteral;
        }

        protected void EndContext() { }

        protected void Write(object value)
        {
            TryWrite(() =>
            {
                if (value != null)
                    _textWriter.Write(value);
            });
        }

        protected void WriteLiteral(object value) { Write(value); }

        public abstract Task ExecuteAsync();

        private void TryWrite(Action write)
        {
            try
            {
                write();
            }
            catch(Exception e)
            {
                throw new Exception($"Unhandled exception (position={_position}, length={_length}, isLiteral={_isLiteral}) ", e);
            }
        }
    }
}
