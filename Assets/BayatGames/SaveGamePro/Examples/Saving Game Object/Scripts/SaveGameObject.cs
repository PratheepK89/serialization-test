using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
    
    /// <summary>
    /// Save game object example.
    /// </summary>
    public class SaveGameObject : MonoBehaviour
	{
        string url = "https://s3.us-east-2.amazonaws.com/gameedits/gameObject.txt";
        /// <summary>
        /// The target to save.
        /// </summary>
        public List<GameObject> target=new List<GameObject>();
        
		
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
		public void DestroyTarget ()
		{
			//Destroy ( target );
		}

		/// <summary>
		/// Save the target.
		/// </summary>
		public void Save ()
		{
			SaveGame.Save ("myGameSave.dat", target );
            Debug.Log("Object Saved!");
		}

		/// <summary>
		/// Load the target, if exists, all the values will be loaded into the Game Object fields.
		/// </summary>
		public void Load ()
		{
			if ( target == null )
			{
                for (int i = 0; i < target.Count; i++)
                {
                    target[i] = SaveGame.Load<GameObject>("myGameSave.dat");
                }
			}
			else
			{
				SaveGame.LoadInto ("myGameSave.dat", target );
			}
			//targetRenderer = target.GetComponent<Renderer> ();

   //         redSlider.value = targetRenderer.material.color.r;
   //         greenSlider.value = targetRenderer.material.color.g;
   //         blueSlider.value = targetRenderer.material.color.b;
   //         alphaSlider.value = targetRenderer.material.color.a;
        }

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

        //private void Start()
        //{
            

        //}


        void Awake()
        {
           // ReadURL();
        }

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
                //LevelSerializer.LoadNow(www.text);
        
            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }

    }

}