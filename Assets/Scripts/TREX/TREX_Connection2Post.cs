//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public class TREX_Connection2Post : TREX_Connection
{
	public TREX_Post post = null;

	public TREX_Connection2Post(HoleType _holeType, MountingPos _mountPos, TREX_Post _post) : base(_holeType, _mountPos)
	{
		post = _post;
	}
}
