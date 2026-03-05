
using UnityEngine;

public interface IAttachable 
{
    bool RequiresInputToAttach { get; }

    public void OnAttach(Component attachableHandler);
    public void HandleAttachedInput(Component attachableHandler);
    public void OnDetach(Component attachableHandler, bool isForced);    
}
