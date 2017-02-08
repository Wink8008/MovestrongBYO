//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public class TREX_Connection
{
	public HoleType holeType = HoleType.None;
	public MountingPos mountPos = MountingPos.None;

	public TREX_Connection(HoleType _holeType, MountingPos _mountPos)
	{
		holeType = _holeType;
		mountPos = _mountPos;
	}

}
