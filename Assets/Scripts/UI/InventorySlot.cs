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
        public bool IsEquipped { get => isEquipped; }

        [SerializeReference]
        public Image equipImg;

        public ItemBase item;

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
        SerializedProperty equipImg;

        
        protected override void OnEnable()
        {
            base.OnEnable();
            itemId = serializedObject.FindProperty("itemId");
            isEquipped = serializedObject.FindProperty("isEquipped");
            equipImg = serializedObject.FindProperty("equipImg");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(itemId);
            EditorGUILayout.PropertyField(isEquipped);
            EditorGUILayout.PropertyField(equipImg);

            serializedObject.ApplyModifiedProperties();

        }
    }
}
