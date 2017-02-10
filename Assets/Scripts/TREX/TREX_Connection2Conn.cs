//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

public class TREX_Connection2Conn : TREX_Connection
{
	public TREX_Connector connector = null;

	public TREX_Connection2Conn(HoleType _holeType, MountingPos _mountPos, TREX_Connector _connector) : base(_holeType, _mountPos)
	{
		connector = _connector;
	}
}
