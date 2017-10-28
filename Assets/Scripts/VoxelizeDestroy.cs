using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace mattatz.VoxelSystem {

    [RequireComponent (typeof(MeshFilter))]
    public class VoxelizeDestroy : MonoBehaviour {

		[SerializeField] [Range(5,20)] 
		int count = 10;
		[SerializeField] 
		int blowPower =10;
		[SerializeField] 
		int radius = 5;
		[SerializeField][Range(1,10)] 
		int massOfParts = 1;

        List<Voxel> voxels;

        void Start () {
            var filter = GetComponent<MeshFilter>();
            voxels = Voxelizer.Voxelize(filter.mesh, count);
			voxels.ForEach (voxel => {
				var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.parent = transform;
				cube.transform.localPosition = voxel.position;
				cube.transform.localScale = voxel.size * Vector3.one;
				cube.transform.localRotation = Quaternion.identity;
				cube.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
				Vector3 explosionPos = cube.transform.localPosition;

				cube.AddComponent<Rigidbody> ();

			});

			for (int i = 0; i < this.transform.childCount; i++) {
				Collider[] colliders = Physics.OverlapSphere(this.transform.GetChild(i).localPosition, radius);
				foreach (Collider hit in colliders) {
					Rigidbody rb = this.transform.GetChild (i).GetComponent<Rigidbody> ();
					rb.mass = massOfParts;
					if (rb != null)
						rb.AddExplosionForce (blowPower, this.transform.GetChild (i).localPosition, radius, 5.0F);
				}
			}

        }
        
        // void Update () {}

//        void OnDrawGizmos () {
//            if (voxels == null) return;
//
//            Gizmos.matrix = transform.localToWorldMatrix;
//            voxels.ForEach(voxel => {
//				Gizmos.DrawCube(voxel.position, voxel.size * new Vector3(20,20,20));
//            });
//        }

    }

}


