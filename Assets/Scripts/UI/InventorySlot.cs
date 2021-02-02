using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace P1.UI
{
    public class InventorySlot : Button
    {
        [Header("InventorySlot Properties")]
        [SerializeField]
        int itemId;
        [SerializeField]
        bool isEquipped = false;

        public ItemBase item;
        public Image equipImg;

        public void SetEquipped(bool isEquipped)
        {
            this.isEquipped = isEquipped;
            equipImg.enabled = isEquipped;
        }
    }

    [CustomEditor(typeof(InventorySlot), true)]
    public class InventorySlotEditor : ButtonEditor
    {
        SerializedProperty itemId;
        SerializedProperty isEquipped;
        protected override void OnEnable()
        {
            base.OnEnable();
            itemId = serializedObject.FindProperty("itemId");
            itemId = serializedObject.FindProperty("isEquipped");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(isEquipped); 
            EditorGUILayout.PropertyField(itemId);
            
        }
    }
}
