using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{

    /// <summary>
    /// Save game object example.
    /// </summary>
    /// 

    // I  created six tile blocks on scene and am trying to change their positions and saving(through Save button on UI) those new positions(for example P1).
    // Then I jumble the boxes and change their positions(say now the positions for boxes are P2) and this time I don't press "Save".
    // When I press "Load" in the scene then I need to get the previously saved positions(P1) on scene.


    public class SaveGameObject : MonoBehaviour
    {
        string url = "https://s3.us-east-2.amazonaws.com/gameedits/gameObject.txt";
        /// <summary>
        /// The target to save.
        /// </summary>
        public List<GameObject> target = new List<GameObject>();


        /// <summary>
        /// The target renderer.
        /// </summary>
        public Renderer targetRenderer;

        /// <summary>
        /// The red slider.
        /// </summary>
        public Slider redSlider;

        /// <summary>
        /// The green slider.
        /// </summary>
        public Slider greenSlider;

        /// <summary>
        /// The blue slider.
        /// </summary>
        public Slider blueSlider;

        /// <summary>
        /// The alpha slider.
        /// </summary>
        public Slider alphaSlider;

        /// <summary>
        /// Update the target renderer color.
        /// </summary>
        //public void UpdateColor ()
        //{
        //	if ( target == null )
        //	{
        //		Debug.LogWarning ( "Target object is destroyed." );
        //		return;
        //	}
        //	if ( targetRenderer == null )
        //	{
        //		targetRenderer = target.GetComponent<Renderer> ();

        //          }
        //          targetRenderer.material.color = new Color(
        //              redSlider.value,
        //              greenSlider.value,
        //              blueSlider.value,
        //              alphaSlider.value);
        //      }

        /// <summary>
        /// Destroy the target.
        /// </summary>
        public void DestroyTarget()
        {
            //Destroy ( target );
        }

        /// <summary>
        /// Save the target.
        /// </summary>
        public void Save()
        {
            SaveGame.Save("gameObject.", target);
            Debug.Log("Object Saved!");
        }

        /// <summary>
        /// Load the target, if exists, all the values will be loaded into the Game Object fields.
        /// </summary>
        public void Load()
        {
            if (target == null)
            {
                for (int i = 0; i < target.Count; i++)
                {
                    target[i] = SaveGame.Load<GameObject>("gameObject.txt");
                }
            }
            else
            {
                SaveGame.LoadInto("gameObject.txt", target);
            }

        }





        // Test implementation for loading "gameObject.txt" file from Amazon s3 bucket when the scene starts
        // As of now ReadURL() is commented for test purpose
        void Awake()
        {
            // ReadURL();
        }


        // Function to load "gameObject.txt" file from Amazon s3 bucket.
        void ReadURL()
        {
            // Debug.Log("Reading file");
            WWW www = new WWW(url);

            StartCoroutine(WaitForRequest(www));
        }

        IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("WWW Ok!");
                CustomizedLoad(www.text);
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }

        // Customized load function , which loads the gameObject.txt file and apply the changes to the game objects on scene
        void CustomizedLoad(string file)
        {
            if (target == null)
            {
                for (int i = 0; i < target.Count; i++)
                {
                    target[i] = SaveGame.Load<GameObject>(file);
                }
            }
            else
            {
                SaveGame.LoadInto(file, target);
            }
        }

    }

}