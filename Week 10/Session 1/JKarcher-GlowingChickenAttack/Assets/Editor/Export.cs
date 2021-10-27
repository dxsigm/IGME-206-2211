using UnityEngine;
using UnityEditor;
/// <summary>
///                                                             Warning
///     This is a custom export script meant to export the equivalent of the entire project as it was at the time of export.
///     This export is intended to compress an entire project for either storage or transfer to another computer.
///     The export will be found in your project folder.
///     
///     As a result this will reset all settings and remove any assets that are in the current project that are not in the 
///     imported project.  Even if you remove all check marks the import will still reset your settings, remove your assets 
///     and just not load in new ones.
///     
///     I have enforced a capital _C at the end of the name of the exported package as a warning that it was through a custom export.
///     
///     If for some reason you need an asset from a project that was exported with this script you should 
///     1. open a new project
///     2. import to the new project
///     3. use the standard export under Assests/Export Package
///     4. either export all assets or just the ones selected
///     5. now you can import the package that you just created in 4. to your project, do not import the package ending with _C.unitypackage
/// </summary>
public class ExportPackage
{
    [MenuItem("Export/MyExport")]
    static void export()
    {
        AssetDatabase.ExportPackage(AssetDatabase.GetAllAssetPaths(),PlayerSettings.productName + "_C.unitypackage", ExportPackageOptions.Interactive | 
                                ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);
    }
}