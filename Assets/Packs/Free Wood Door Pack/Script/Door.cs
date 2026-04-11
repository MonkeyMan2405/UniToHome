using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour, IInteractable
{
	public bool open;
	public float smooth = 1.0f;
	float DoorOpenAngle = -90.0f;
	float DoorCloseAngle = 0.0f;



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



		if (open)
		{
			SoundManager.PlaySoundAt(SoundType.DoorOpen, 1f, 0.5f, gameObject.transform);
		}
		else
		{
			SoundManager.PlaySoundAt(SoundType.DoorClose, 1f, 0.5f, gameObject.transform);
		}

	}



	public void Interact()
	{
		if (open)
		{
			open = false;
			SoundManager.PlaySoundAt(SoundType.DoorClose, 1f, 0.5f, gameObject.transform);
		}
		else
		{
			open = true;
			SoundManager.PlaySoundAt(SoundType.DoorOpen, 1f, 0.5f, gameObject.transform);
		}
	}



	public void MonsterSlam()
	{
        SoundManager.PlaySoundAt(SoundType.DoorSlam, 1.5f, 0.5f, gameObject.transform);
    }



}




