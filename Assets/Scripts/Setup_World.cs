using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup_World : MonoBehaviour {

	[SerializeField]
	private GUIStyle guiStyle;

	GameObject instance;
	GameObject voxel;
	ParticleSystem particle;
	SkinnedMeshRenderer rend;

	void Start()
	{
		// Instantiates a prefab named "enemy" located in any Resources
		// folder in project's Assets folder.
		instance = Instantiate(Resources.Load("FlyUnit_lowPoly", typeof(GameObject))) as GameObject;
		instance.transform.position = new Vector3 (0, -5);
		instance.transform.localScale = new Vector3 (3, 3, 3);

		Animator anim = instance.GetComponent<Animator> ();
		anim.runtimeAnimatorController = Resources.Load ("FlyUnit_ac") as RuntimeAnimatorController;

		rend = instance.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
		rend.material.mainTexture = Resources.Load("sss") as Texture;




	}

	void LateUpdate()
	{
		instance.transform.RotateAround (instance.transform.position, Vector3.up, .5f);
		if (particle.isStopped && particle != null) {
			Destroy (GameObject.FindGameObjectWithTag ("ps"));
		} 
	}

	void OnGUI() {
		//GUI.color = Color.red;
		guiStyle.fontSize = 25; //change the font size
		if(instance!=null){
			if (GUI.Button (new Rect (0, 0, Screen.width/2, Screen.height/6), "Destroy", guiStyle)) {
				this.gameObject.GetComponent<AudioSource> ().Play ();
				Transform temp = instance.transform;
			particle = Instantiate(Resources.Load("Particle System", typeof(ParticleSystem))) as ParticleSystem;
			Destroy (instance);
					voxel = Instantiate (Resources.Load ("VoxeldFlyUnit", typeof(GameObject))) as GameObject;
				voxel.transform.position = temp.transform.position;
		}
		}

		if(particle!=null){
			if (GUI.Button (new Rect (0, 0, Screen.width/2, Screen.height/6),  "One more FlyUnit", guiStyle)) {
				Destroy (voxel);
				instance = Instantiate(Resources.Load("FlyUnit_lowPoly", typeof(GameObject))) as GameObject;
				instance.transform.position = new Vector3 (0, -5);
				instance.transform.localScale = new Vector3 (3, 3, 3);
				rend = instance.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
				rend.material.mainTexture = Resources.Load("sss") as Texture;
			}
		}

		if (GUI.Button (new Rect (Screen.width/2, 1, Screen.width/2, Screen.height/6), "Quit", guiStyle)) {
			System.Diagnostics.Process.GetCurrentProcess().Kill();
			Application.Quit ();
		}
	}
}
