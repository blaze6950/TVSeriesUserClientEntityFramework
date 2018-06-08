using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    class PresenterMainWindow : IPresenterMainWindow
    {
        private IViewMainWindow _view;
        private TVSeriesModel _model;
        public PresenterMainWindow(IViewMainWindow view)
        {
            _view = view;
            _model = new TVSeriesModel();
        }

        public void LoginClick()
        {
            throw new NotImplementedException();
        }

        public void NotYetRegisteredClick()
        {
            throw new NotImplementedException();
        }

        public void ForgetPasswordClick()
        {
            throw new NotImplementedException();
        }
    }
}
