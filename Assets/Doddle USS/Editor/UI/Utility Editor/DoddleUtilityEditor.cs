using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss.editor
{
    public class DoddleUtilityEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset configEditorUXML = default;

        [SerializeField]
        private VisualTreeAsset utilityUXML = default;
        
        [SerializeField]
        private VisualTreeAsset propertyUXML = default;

        [SerializeField]
        private VisualTreeAsset btnPlusUXML = default;

        [SerializeField]
        private VisualTreeAsset btnDeleteUXML = default;

        [SerializeField, Tooltip("Scriptable Object DoddleUtilityData which stores the current config for all USS classes in doddle-utility.uss")]
        private DoddleUtilityData utilityData = default;

        [SerializeField, Tooltip("doddle-utility.uss which has all utility classes")]
        private StyleSheet utilitiesUSS = default;

        [SerializeField, Tooltip("JSON file containing the original config of doddle-utility.uss")]
        private TextAsset utilityBackup = default;

        [SerializeField, Tooltip("JSON file containing the exported config of doddle-utility.uss")]
        private TextAsset utilityExport = default;

        [SerializeField, Tooltip("JSON file containing the imported config of doddle-utility.uss")]
        private TextAsset utilityImport = default;

        private static readonly string btnUpdateName = "btn-update-utilities";
        private static readonly string btnRestoreName = "btn-restore-utilities";
        private static readonly string btnExportName = "btn-export-utilities";
        private static readonly string btnImportName = "btn-import-utilities";
        private static readonly string btnCreateName = "btn-create-utility";
        private static readonly string scrollViewName = "scroll-view";
        private static readonly string foldoutLabelContainerClass = "unity-foldout__input";
        private static readonly string btnPlusClass = "btn-plus";
        private static readonly string btnDeleteClass = "btn-delete";

        public static readonly string FoldoutContainerName = "unity-content";
        public static readonly string UtilityClass = "foldout-editor-utility";
        public static readonly string UtilityName = "utility-name";
        public static readonly string UtilityIdentifier = "utility-identifier";
        public static readonly string UtilityInstance = "utility-instances";
        public static readonly string PropertyClass = "foldout-editor-property";
        public static readonly string PropertyName = "property-name";
        public static readonly string PropertyValue = "property-value";
        public static readonly string PropertyUnit = "property-unit";

        private VisualElement root;
        private ScrollView scrollView;

        [MenuItem("Window/Doddle USS/Utility Editor")]
        public static void ShowWindow()
        {
            DoddleUtilityEditor wnd = GetWindow<DoddleUtilityEditor>();
            wnd.titleContent = new GUIContent("Doddle Utility Editor");
        }

        public void CreateGUI()
        {
            VisualElement baseUXML = configEditorUXML.Instantiate();
            root = rootVisualElement;
            root.Add(baseUXML);

            Button btnUpdate = root.Q<Button>(btnUpdateName);
            Button btnRestore = root.Q<Button>(btnRestoreName);
            Button btnExport = root.Q<Button>(btnExportName);
            Button btnImport = root.Q<Button>(btnImportName);
            Button btnCreate = root.Q<Button>(btnCreateName);
            scrollView = root.Q<ScrollView>(scrollViewName);

            btnRestore.clicked += () => 
            { 
                JsonHelper.RestoreDefaults(utilityBackup, utilityData);
                PopulateUtilityFoldouts();
                UpdateUtilities(utilitiesUSS);
            };

            btnImport.clicked += () => 
            { 
                JsonHelper.ImportConfig(utilityImport, utilityData);
                PopulateUtilityFoldouts();
                UpdateUtilities(utilitiesUSS);
            };

            btnExport.clicked += () => 
            {
                utilityData.Utilities = UtilityDataMutator.GetUtilityDataFromFoldouts(scrollView);
                JsonHelper.ExportConfig(utilityExport, utilityData);
            };

            btnUpdate.clicked += () => 
            {
                utilityData.Utilities = UtilityDataMutator.GetUtilityDataFromFoldouts(scrollView);
                UpdateUtilities(utilitiesUSS);
            };

            btnCreate.clicked += () =>
            {
                VisualElement utilityContentContainer = CloneUtilityFoldout(new Utility("", 1));
                VisualElement propertyTemplate = CloneBasePropertyFoldout();
                utilityContentContainer.Add(propertyTemplate);
            };

            PopulateUtilityFoldouts();
        }

        /// <summary>
        /// Get the contents of the utilities foldout from UI into a string, gets fille path for USS file, 
        /// and writes to the USS file
        /// </summary>
        /// <param name="textAsset"></param>
        private void UpdateUtilities(Object textAsset)
        {
            string contents = UtilityDataMutator.BuildUtilityContents(utilityData.Utilities);
            string path = FileReader.GetFilePath(textAsset);

            FileWriter.WriteToFile(path, contents);
            Debug.Log("DoddleConfigEditor.cs - UtilityData successfully updated");
            Debug.Log("DoddleConfigEditor.cs - doddle-utility.uss successfully updated");
        }

        /// <summary>
        /// Populates the ScrollView with foldouts representing utility classes. Each Utility class has 1 
        /// or more Property foldouts
        /// </summary>
        private void PopulateUtilityFoldouts()
        {
            scrollView.Clear();

            //Create foldouts for utilities
            foreach (Utility utility in utilityData.Utilities)
            {
                VisualElement utilityContentContainer = CloneUtilityFoldout(utility);

                //Create foldouts for properties
                foreach (Property property in utility.Properties)
                {
                    VisualElement propertyTemplate = ClonePropertyFoldout(property);
                    utilityContentContainer.Add(propertyTemplate);
                }
            }
        }

        /// <summary>
        /// Creates a new utility foldout from the template, populates buttons, and info
        /// </summary>
        /// <param name="utility"></param>
        /// <returns></returns>
        private VisualElement CloneUtilityFoldout(Utility utility)
        {
            //Get references to the containers in the foldout for the buttons, and base fields
            VisualElement utilitytemplate = utilityUXML.CloneTree();
            VisualElement utilityContentContainer = utilitytemplate.Q<VisualElement>(FoldoutContainerName);
            VisualElement utilityLabelContainer = utilitytemplate.Q<VisualElement>(className: foldoutLabelContainerClass);
            
            //Set label to utility class name
            Label utilityLabel = utilitytemplate.Q<Label>();

            if (string.IsNullOrWhiteSpace(utility.ClassName))
            {
                utilityLabel.text = "New Utility".ToUpper();
            }
            else
            {
                utilityLabel.text = utility.ClassName.ToUpper();
            }

            //Get references to the text fields and int fields in the foldout
            TextField className = utilitytemplate.Q<TextField>(UtilityName);
            IntegerField instances = utilitytemplate.Q<IntegerField>(UtilityInstance);

            //Add close button and plus button
            ClonePlusBtn(utilityLabelContainer, utilityContentContainer);
            CloneDeleteBtn(utilityLabelContainer, utilitytemplate);

            //Set base field info
            className.value = utility.ClassName;
            instances.value = utility.Instances;

            scrollView.Add(utilitytemplate);
            return utilityContentContainer;
        }

        /// <summary>
        /// Creates a new property foldout from the template, populates buttons, and info
        /// </summary>
        /// <param name="property"></param>
        private VisualElement ClonePropertyFoldout(Property property)
        {
            //Get references to the text fields in the foldout
            VisualElement propertyTemplate = CloneBasePropertyFoldout();
            TextField name = propertyTemplate.Q<TextField>(PropertyName);
            TextField value = propertyTemplate.Q<TextField>(PropertyValue);
            TextField unit = propertyTemplate.Q<TextField>(PropertyUnit);

            name.value = property.Name;
            value.value = property.Value;
            unit.value = property.Unit;

            return propertyTemplate;
        }

        /// <summary>
        /// Creates a new property foldout from the template but does not populate info. This is a base clone for plus button
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private VisualElement CloneBasePropertyFoldout()
        {
            //Get references to the containers in the foldout for the buttons, and base fields
            VisualElement propertyTemplate = propertyUXML.CloneTree();
            VisualElement propertyLabelContainer = propertyTemplate.Q<VisualElement>(className: foldoutLabelContainerClass);

            //Add close button
            CloneDeleteBtn(propertyLabelContainer, propertyTemplate);

            return propertyTemplate;
        }

        /// <summary>
        /// Creates a new delete button on the utility or property foldout. Removes parent from hierarchy when clicked
        /// </summary>
        /// <param name="parent"></param>
        private void CloneDeleteBtn(VisualElement parent, VisualElement elementToDelete)
        {
            parent.CreateChildTemplate(btnDeleteUXML, 1);
            Button btn = parent.Q<Button>(className: btnDeleteClass);

            btn.clicked += () =>
            {
                elementToDelete.RemoveFromHierarchy();
            };
        }

        /// <summary>
        /// Creates a new plus button on the utility foldout. Creates a new property foldout when clicked
        /// </summary>
        /// <param name="parent"></param>
        private void ClonePlusBtn(VisualElement parent, VisualElement contentContainer)
        {
            parent.CreateChildTemplate(btnPlusUXML, 1);
            Button btn = parent.Q<Button>(className: btnPlusClass);

            btn.clicked += () =>
            {
                VisualElement propertyTemplate = CloneBasePropertyFoldout();
                contentContainer.Add(propertyTemplate);
            };
        }
    }
}