using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM;

//public class ViewModelResolver
//{
//    //private Func<ViewModelBase, Type> resolve;

//    public ViewModelResolver(Func<ViewModelBase, Type> resolve)
//    {
//        //Resolve = resolve;
//        Resolve = new Resolve(resolve);
//    }
//private Func<ViewModelBase, Type> _function;

public delegate Type ViewModelResolver(ViewModelBase viewModel);
    //public ViewModelResolver(Func<ViewModelBase,Type> function)
    //{
    //    _function = function;
    //}

    //public Type Resolve(ViewModelBase vm)
    //{
    //    return _function(vm).invok;
    //}
//}
