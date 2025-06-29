using System;
using System.Collections.Generic;

//A basic C# Event System
public static class EventHandler
{
    public static event Action E_BeforeUnloadScene;
    public static void Call_BeforeUnloadScene() { E_BeforeUnloadScene?.Invoke(); }
    public static event Action E_AfterLoadScene;
    public static void Call_AfterLoadScene() { E_AfterLoadScene?.Invoke(); }
    public static event Action E_OnBeginSave;
    public static void Call_OnBeginSave() => E_OnBeginSave?.Invoke();
    public static event Action E_OnCompleteSave;
    public static void Call_OnCompleteSave() => E_OnCompleteSave?.Invoke();

    #region Interaction
    public static event Action E_OnTrashDeads;
    public static void Call_OnBeginTrash() => E_OnTrashDeads?.Invoke();
    public static event Action<DragableNotes> E_OnInsertLabel;
    public static void Call_OnInsertLabel(DragableNotes notes) => E_OnInsertLabel?.Invoke(notes);
    public static event Action<string> E_OnChooseLabel;
    public static void Call_OnChooseLabel(string label) => E_OnChooseLabel?.Invoke(label);
    public static event Action<LabelDetailData> E_OnShowLabel;
    public static void Call_OnShowLabel(LabelDetailData detailData) => E_OnShowLabel?.Invoke(detailData);
    public static event Action E_AfterReadLabel;
    public static void Call_AfterReadLabel() => E_AfterReadLabel?.Invoke();
    public static event Action E_FlushInput;
    public static void Call_FlushInput() => E_FlushInput?.Invoke();
    #endregion
}

//A More Strict Event System
namespace SimpleEventSystem{
    public abstract class Event{
        public delegate void Handler(Event e);
    }
    public class E_OnTestEvent:Event{
        public float value;
        public E_OnTestEvent(float data){value = data;}
    }

    public class EventManager{
        private static  EventManager instance;
        public static EventManager Instance{
            get{
                if(instance == null) instance = new EventManager();
                return instance;
            }
        }

        private Dictionary<Type, Event.Handler> RegisteredHandlers = new Dictionary<Type, Event.Handler>();
        public void Register<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);

            if(RegisteredHandlers.ContainsKey(type)){
                RegisteredHandlers[type] += handler;
            }
            else{
                RegisteredHandlers[type] = handler;
            }
        }
        public void UnRegister<T>(Event.Handler handler) where T: Event{
            Type type = typeof(T);
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers -= handler;
                if(handlers == null){
                    RegisteredHandlers.Remove(type);
                }
                else{
                    RegisteredHandlers[type] = handlers;
                }
            }
        }
        public void FireEvent<T>(T e) where T:Event {
            Type type = e.GetType();
            Event.Handler handlers;

            if(RegisteredHandlers.TryGetValue(type, out handlers)){
                handlers?.Invoke(e);
            }
        }
        public void ClearList(){RegisteredHandlers.Clear();}
    }
}
