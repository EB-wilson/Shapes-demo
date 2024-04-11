using Shapes.Components;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Shapes.Events
{
    public interface IDamageTarget: IEventSystemHandler
    {
        void doDamaged(Health target, int damage);
        void death(Health target);
    }
}