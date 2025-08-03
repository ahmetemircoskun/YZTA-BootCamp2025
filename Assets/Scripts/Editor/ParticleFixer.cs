using UnityEngine;
using UnityEditor;
using System.IO;

public class ParticleFixer : MonoBehaviour
{
    [MenuItem("Tools/Fix All Particle Materials")]
    static void FixParticles()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;

        Material urpParticleMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/URP_ParticleMaterial.mat");
        if (urpParticleMat == null)
        {
            Debug.LogError("URP_ParticleMaterial.mat bulunamadı! Dosya yolunu kontrol et.");
            return;
        }

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            ParticleSystemRenderer[] renderers = prefab.GetComponentsInChildren<ParticleSystemRenderer>(true);
            bool changed = false;

            foreach (var psr in renderers)
            {
                if (psr.sharedMaterial == null || psr.sharedMaterial.shader.name.Contains("Legacy"))
                {
                    psr.sharedMaterial = urpParticleMat;
                    changed = true;
                }
            }

            if (changed)
            {
                count++;
                PrefabUtility.SavePrefabAsset(prefab);
            }
        }

        Debug.Log($"Toplam {count} prefab güncellendi.");
    }
}
