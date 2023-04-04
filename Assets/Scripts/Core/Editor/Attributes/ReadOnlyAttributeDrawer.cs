#region

using Core.Attribute;
using UnityEditor;
using UnityEngine;

#endregion

namespace Core.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            GUI.enabled = false;
            base.OnGUI(position);
        }


        public override float GetHeight()
        {
            return 0;
        }
    }
}