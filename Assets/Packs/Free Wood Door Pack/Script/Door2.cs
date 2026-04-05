using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door2 : MonoBehaviour, IInteractable
	{
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = 90f;
		float DoorCloseAngle = 0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		public Door3 door3Ref;

		public bool toggle;

        void Start()
		{
			asource = GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update()
		{
			if (open)
			{
				var target = Quaternion.Euler(0, DoorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);

			}
			else
			{
				var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);

			}
		}

		public void OpenDoor()
		{
			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
		}



		public void Interact()
		{
			if (open)
			{
				open = false;
            }
			else
			{
				open = true;

            }

			Debug.Log("Door Interacted");
			

        }
	}
}