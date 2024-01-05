
//namespace Line.Messaging;
//public interface ILineHttpIReceive : IReceive<HttpRequestLogDTO> { }
//public interface ILineHttpNotify : INotify<HttpRequestLogDTO> { }
//public class LineHttpNotify : ILineHttpNotify
//{
//    readonly List<ILineHttpIReceive> observers;
//    public LineHttpNotify(IEnumerable<ILineHttpIReceive> obs)
//    {
//        observers = new List<ILineHttpIReceive>();
//        if (obs != null)
//        {
//            foreach (var item in obs)
//            {
//                observers.Add(item);
//            }
//        }
//    }
//    public void Detach(ILineHttpIReceive observer)
//    {
//        observers.Remove(observer);
//    }

//    public void Notify(HttpRequestLogDTO data)
//    {
//        foreach (var item in observers)
//        {
//            item.OnNext(data);
//        }
//    }
//}
