//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager_TREX : FrameManager 
{

	public void Build4Post()
	{
		if(parts.Length > 0)
			DestroyAllParts ();

		// spawn post1
		GameObject post1 = SpawnPart((byte)TREX_PartID.Post_T3500);

		// spawn monkeyBarBridgeMount_1
		GameObject monkeyBarBridgeMount_1 = SpawnPart ((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, post1);

		// spawn post2
		GameObject post2 = SpawnPart((byte)TREX_PartID.Post_T3000, monkeyBarBridgeMount_1);

		// create connections to post1 & post2 on monkeyBarBridgeMount_1
		BaseSelectable mbbm_BS = monkeyBarBridgeMount_1.GetComponent<BaseSelectable>();
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post1);
		mbbm_BS.CreateConnectionData (HoleType.High, MountingPos.West, post2);

		// create connections to monkeyBarBridgeMount_1 on post1 & post2
		BaseSelectable post_BS = post1.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_1);
		post_BS = post2.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.North, monkeyBarBridgeMount_1);

		// set monkeyBarBridgeMount_1 as parent to post1 & post2
		// set post1 & post2 as children of monkeyBarBridgeMount_1
		SetParentChild (monkeyBarBridgeMount_1, post1);
		SetParentChild (monkeyBarBridgeMount_1, post2);

		// spawn monkeyBarBridge
		GameObject monkeyBarBridge = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridge, monkeyBarBridgeMount_1);

		// spawn monkeyBarBridgeMount_2
		GameObject monkeyBarBridgeMount_2 = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, monkeyBarBridge);

		// set monkeyBarBridge as parent to monkeyBarBridgeMount_1 & monkeyBarBridgeMount_2
		// set monkeyBarBridgeMount_1 & monkeyBarBridgeMount_2 as children of monkeyBarBridge
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_1);
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_2);

		// spawn post3
		GameObject post3 = SpawnPart((byte)TREX_PartID.Post_T3000, monkeyBarBridgeMount_2);

		// spawn post4
		GameObject post4 = SpawnPart((byte)TREX_PartID.Post_T3500, monkeyBarBridgeMount_2);

		// create connections to post3 & post4 on monkeyBarBridgeMount_2
		mbbm_BS = monkeyBarBridgeMount_2.GetComponent<BaseSelectable>();
		mbbm_BS.CreateConnectionData (HoleType.High, MountingPos.West, post3);
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post4);

		// create connections to monkeyBarBridgeMount_2 on post3 & post4
		post_BS = post3.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.North, monkeyBarBridgeMount_2);
		post_BS = post4.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_2);

		// set monkeyBarBridgeMount_2 as parent to post3 & post4
		// set post3 & post4 as children of monkeyBarBridgeMount_2
		SetParentChild (monkeyBarBridgeMount_2, post3);
		SetParentChild (monkeyBarBridgeMount_2, post4);
	}

	public void Build5Post()
	{
		if(parts.Length > 0)
			DestroyAllParts ();

		// spawn post1
		GameObject post1 = SpawnPart((byte)TREX_PartID.Post_T2200);

		// spawn pullup_1
		GameObject pullup_1 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.North);

		// spawn pullup_2
		GameObject pullup_2 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.East);

		// spawn pullup_3
		GameObject pullup_3 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.South);

		// spawn pullup_4
		GameObject pullup_4 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.West);

		// set post1 as parent to pullup_1, pullup_2, pullup_3 & pullup_4
		// set pullup_1, pullup_2, pullup_3 & pullup_4 as children of post1
		SetParentChild (post1, pullup_1);
		SetParentChild (post1, pullup_2);
		SetParentChild (post1, pullup_3);
		SetParentChild (post1, pullup_4);

		// create connections to pullup_1, pullup_2, pullup_3 & pullup_4 on post1
		BaseSelectable post_BS = post1.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, pullup_1);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.East, pullup_2);
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.West, pullup_4);

		// create connections to post1 on pullup_1, pullup_2, pullup_3 & pullup_4
		BaseSelectable pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post1);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post1);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.North, post1);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post1);

		// spawn post2
		GameObject post2 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_1);

		// spawn post3
		GameObject post3 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_2);

		// spawn post4
		GameObject post4 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_3);

		// spawn post5
		GameObject post5 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_4);

		SetParentChild (pullup_1, post2);
		SetParentChild (pullup_2, post3);
		SetParentChild (pullup_3, post4);
		SetParentChild (pullup_4, post5);

		post_BS = post2.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_1);

		post_BS = post3.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_2);

		post_BS = post4.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);

		post_BS = post5.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_4);

		pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post2);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post3);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post4);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post5);
	}

	public void Build7Post()
	{
		if(parts.Length > 0)
			DestroyAllParts ();

		// spawn post1
		GameObject post1 = SpawnPart((byte)TREX_PartID.Post_T2200);

		// spawn pullup_1
		GameObject pullup_1 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.North);

		// spawn pullup_2
		GameObject pullup_2 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.East);

		// spawn pullup_3
		GameObject pullup_3 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.South);

		// spawn pullup_4
		GameObject pullup_4 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.West);

		// set post1 as parent to pullup_1, pullup_2, pullup_3 & pullup_4
		// set pullup_1, pullup_2, pullup_3 & pullup_4 as children of post1
		SetParentChild (post1, pullup_1);
		SetParentChild (post1, pullup_2);
		SetParentChild (post1, pullup_3);
		SetParentChild (post1, pullup_4);

		// create connections to pullup_1, pullup_2, pullup_3 & pullup_4 on post1
		BaseSelectable post_BS = post1.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, pullup_1);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.East, pullup_2);
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.West, pullup_4);

		// create connections to post1 on pullup_1, pullup_2, pullup_3 & pullup_4
		BaseSelectable pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post1);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post1);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.North, post1);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post1);

		// spawn post2
		GameObject post2 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_1);

		// spawn post3
		GameObject post3 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_2);

		// spawn post4
		GameObject post4 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_3);

		// spawn post5
		GameObject post5 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_4);

		SetParentChild (pullup_1, post2);
		SetParentChild (pullup_2, post3);
		SetParentChild (pullup_3, post4);
		SetParentChild (pullup_4, post5);

		post_BS = post2.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_1);

		post_BS = post3.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_2);

		post_BS = post4.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);

		post_BS = post5.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_4);

		pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post2);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post3);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post4);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post5);

		post_BS = post4.GetComponent<BaseSelectable> ();

		// spawn monkeyBarBridgeMount_1
		GameObject monkeyBarBridgeMount_1 = SpawnPart ((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, post4);
		BaseSelectable mbbm_BS = monkeyBarBridgeMount_1.GetComponent<BaseSelectable> ();

		// create connection to monkeyBarBridgeMount_1 on post4
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_1);
		// create connection to post4 on monkeyBarBridgeMount_1
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post4);

		// spawn monkeyBarBridge
		GameObject monkeyBarBridge = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridge, monkeyBarBridgeMount_1);

		// set monkeyBarBridge as parent to monkeyBarBridgeMount_1
		// set monkeyBarBridgeMount_1 as child of monkeyBarBridge
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_1);

		// spawn monkeyBarBridgeMount_2
		GameObject monkeyBarBridgeMount_2 = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, monkeyBarBridge);

		// set monkeyBarBridge as parent to monkeyBarBridgeMount_2
		// set monkeyBarBridgeMount_2 as child of monkeyBarBridge
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_2);

		// spawn post6
		GameObject post6 = SpawnPart((byte)TREX_PartID.Post_T3000, monkeyBarBridgeMount_2);

		// spawn post7
		GameObject post7 = SpawnPart((byte)TREX_PartID.Post_T3500, monkeyBarBridgeMount_2);

		// create connections to post6 & post7 on monkeyBarBridgeMount_2
		mbbm_BS = monkeyBarBridgeMount_2.GetComponent<BaseSelectable>();
		mbbm_BS.CreateConnectionData (HoleType.High, MountingPos.West, post6);
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post7);

		// create connections to monkeyBarBridgeMount_2 on post6 & post7
		post_BS = post6.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.North, monkeyBarBridgeMount_2);
		post_BS = post7.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_2);

		// set monkeyBarBridgeMount_2 as parent to post6 & post7
		// set post6 & post7 as children of monkeyBarBridgeMount_2
		SetParentChild (monkeyBarBridgeMount_2, post6);
		SetParentChild (monkeyBarBridgeMount_2, post7);
	}

	public void Build10Post()
	{
		if(parts.Length > 0)
			DestroyAllParts ();

		// spawn post1
		GameObject post1 = SpawnPart((byte)TREX_PartID.Post_T2200);

		// spawn pullup_1
		GameObject pullup_1 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.North);

		// spawn pullup_2
		GameObject pullup_2 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.East);

		// spawn pullup_3
		GameObject pullup_3 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.South);

		// spawn pullup_4
		GameObject pullup_4 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post1, MountingPos.West);

		// set post1 as parent to pullup_1, pullup_2, pullup_3 & pullup_4
		// set pullup_1, pullup_2, pullup_3 & pullup_4 as children of post1
		SetParentChild (post1, pullup_1);
		SetParentChild (post1, pullup_2);
		SetParentChild (post1, pullup_3);
		SetParentChild (post1, pullup_4);

		// create connections to pullup_1, pullup_2, pullup_3 & pullup_4 on post1
		BaseSelectable post_BS = post1.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, pullup_1);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.East, pullup_2);
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.West, pullup_4);

		// create connections to post1 on pullup_1, pullup_2, pullup_3 & pullup_4
		BaseSelectable pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post1);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post1);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.North, post1);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post1);

		// spawn post2
		GameObject post2 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_1);

		// spawn post3
		GameObject post3 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_2);

		// spawn post4
		GameObject post4 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_3);

		// spawn post5
		GameObject post5 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_4);

		SetParentChild (pullup_1, post2);
		SetParentChild (pullup_2, post3);
		SetParentChild (pullup_3, post4);
		SetParentChild (pullup_4, post5);

		post_BS = post2.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_1);

		post_BS = post3.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_2);

		post_BS = post4.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_3);

		post_BS = post5.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_4);

		pullupBS = pullup_1.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post2);
		pullupBS = pullup_2.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post3);
		pullupBS = pullup_3.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.South, post4);
		pullupBS = pullup_4.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.South, post5);

		post_BS = post4.GetComponent<BaseSelectable> ();

		// spawn monkeyBarBridgeMount_1
		GameObject monkeyBarBridgeMount_1 = SpawnPart ((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, post4);
		BaseSelectable mbbm_BS = monkeyBarBridgeMount_1.GetComponent<BaseSelectable> ();

		// create connection to monkeyBarBridgeMount_1 on post4
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_1);
		// create connection to post4 on monkeyBarBridgeMount_1
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post4);

		// spawn monkeyBarBridge
		GameObject monkeyBarBridge = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridge, monkeyBarBridgeMount_1);

		// set monkeyBarBridge as parent to monkeyBarBridgeMount_1
		// set monkeyBarBridgeMount_1 as child of monkeyBarBridge
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_1);

		// spawn monkeyBarBridgeMount_2
		GameObject monkeyBarBridgeMount_2 = SpawnPart((byte)TREX_PartID.Conn_MonkeyBarBridgeMount, monkeyBarBridge);

		// set monkeyBarBridge as parent to monkeyBarBridgeMount_2
		// set monkeyBarBridgeMount_2 as child of monkeyBarBridge
		SetParentChild (monkeyBarBridge, monkeyBarBridgeMount_2);

		// spawn post6
		GameObject post6 = SpawnPart((byte)TREX_PartID.Post_T3000, monkeyBarBridgeMount_2);

		// spawn post7
		GameObject post7 = SpawnPart((byte)TREX_PartID.Post_T3500, monkeyBarBridgeMount_2);

		// create connections to post6 & post7 on monkeyBarBridgeMount_2
		mbbm_BS = monkeyBarBridgeMount_2.GetComponent<BaseSelectable>();
		mbbm_BS.CreateConnectionData (HoleType.High, MountingPos.West, post6);
		mbbm_BS.CreateConnectionData (HoleType.Low, MountingPos.East, post7);

		// create connections to monkeyBarBridgeMount_2 on post6 & post7
		post_BS = post6.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.North, monkeyBarBridgeMount_2);
		post_BS = post7.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, monkeyBarBridgeMount_2);

		// set monkeyBarBridgeMount_2 as parent to post6 & post7
		// set post6 & post7 as children of monkeyBarBridgeMount_2
		SetParentChild (monkeyBarBridgeMount_2, post6);
		SetParentChild (monkeyBarBridgeMount_2, post7);

		// spawn pullup_5
		GameObject pullup_5 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post6, MountingPos.South);

		// spawn post8
		GameObject post8 = SpawnPart((byte)TREX_PartID.Post_T2200, pullup_5);

		// create connections to pullup_5 on post6
		post_BS = post6.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_5);

		// create connections to pullup_5 on post8
		post_BS = post8.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.East, pullup_5);

		// create connections to post6 & post8 on pullup_5
		pullupBS = pullup_5.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post6);
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post8);

		// spawn pullup_6
		GameObject pullup_6 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post8, MountingPos.South);

		// spawn pullup_7
		GameObject pullup_7 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post8, MountingPos.West);

		// spawn pullup_8
		GameObject pullup_8 = SpawnPart ((byte)TREX_PartID.Conn_PullUp_Std, post8, MountingPos.North);

		// create connections to pullup_6, pullup_7 & pullup_8 on post8
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_6);
		post_BS.CreateConnectionData (HoleType.High, MountingPos.West, pullup_7);
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.North, pullup_8);

		// set post8 as parent to pullup_5, pullup_6, pullup_7 & pullup_8
		// set pullup_5, pullup_6, pullup_7 & pullup_8 as children of post8
		SetParentChild (post8, pullup_5);
		SetParentChild (post8, pullup_6);
		SetParentChild (post8, pullup_7);
		SetParentChild (post8, pullup_8);

		// spawn post9
		GameObject post9 = SpawnPart((byte)TREX_PartID.Post_T3500, pullup_6);

		// spawn post10
		GameObject post10 = SpawnPart((byte)TREX_PartID.Post_T3000, pullup_7);

		// create connections to pullup_6 on post9
		post_BS = post9.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.Low, MountingPos.South, pullup_6);

		// create connections to pullup_7 on post10
		post_BS = post10.GetComponent<BaseSelectable> ();
		post_BS.CreateConnectionData (HoleType.High, MountingPos.South, pullup_7);

		// create connections to post8 & post9 on pullup_6
		pullupBS = pullup_6.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post8);
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post9);

		// create connections to post8 & post10 on pullup_7
		pullupBS = pullup_7.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.East, post8);
		pullupBS.CreateConnectionData (HoleType.Low, MountingPos.West, post10);

		// create connections to post8 & post7 on pullup_8
		pullupBS = pullup_7.GetComponent<BaseSelectable> ();
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.East, post8);
		pullupBS.CreateConnectionData (HoleType.High, MountingPos.West, post7);

		SetParentChild (pullup_6, post9);
		SetParentChild (pullup_7, post10);
	}

	public void Build12Post()
	{
		
	}

	public override void TogglePart (GameObject selectedPart, byte spawnID)
	{
		TREX_Post sPost = selectedPart.GetComponent<TREX_Post> ();
		TREX_Connector sConn = selectedPart.GetComponent<TREX_Connector> ();
		if (sPost)
		{	
			if (TREX_Post.IsAPost ((TREX_PartID)spawnID))
			{
				if(sPost.PartID.Equals((TREX_PartID)spawnID))
				{
					// user is clicking on the button of an active post (ie: post T4600, T6400 or T6500)
					// we dont want to respawn the same post so just return.
					return;
				}
				// trying to spawn a new post
				GameObject newPostGO = SpawnPart (spawnID, sPost.ParentPart);
				TREX_Post sNewPost = newPostGO.GetComponent<TREX_Post> ();
				SetParentChild (sPost.ParentPart, newPostGO);
				sPost.TransferOrDestroyChildren (newPostGO.GetComponent<TREX_Post> ());

				BaseSelectable selectedParentBS = sPost.ParentPart.GetComponent<BaseSelectable> ();
				selectedParentBS.ChildrenParts.Remove (selectedPart);
				Destroy (selectedPart);

				DropdownMenu_TREX trexDropMenu = SceneManager.Instance.DropMenu as DropdownMenu_TREX;
				trexDropMenu.DeleteItemsForPart (sPost);
				trexDropMenu.CreateItemsFor (sNewPost);
				//trexDropMenu.ReorderMenuItems ();
				trexDropMenu.ResizeParent ();

				List<byte> activeChildrenTypes = sNewPost.GetActiveChildrenTypes ();
				foreach (byte partID in activeChildrenTypes)
					trexDropMenu.SetActive_ItemsForPart (sNewPost, partID);
			} 
			else if (TREX_Part.IsAPart ((TREX_PartID)spawnID))
			{
				// trying to spawn a new part
				List<byte> compatPartIDs = sPost.GetCompatibleParts ();
				if (compatPartIDs.Contains (spawnID))
				{
					if (sPost.HasChildPart (spawnID))
					{
						// post has spawn part already. destroy child.
						sPost.DestroyChild (spawnID);
					} else
					{
						// post does not have spawn part. spawn child.
						sPost.SpawnChild (spawnID);
					}
				} 
				else
				{
					TREX_PartID newPostID = TREX_Post.GetPostForPart ((TREX_PartID)spawnID);
					GameObject newPostGO = SpawnPart ((byte)newPostID, sPost.ParentPart);
					TREX_Post sNewPost = newPostGO.GetComponent<TREX_Post> ();
					SetParentChild (sPost.ParentPart, newPostGO);
					sPost.TransferOrDestroyChildren (newPostGO.GetComponent<TREX_Post> ());

					BaseSelectable selectedParentBS = sPost.ParentPart.GetComponent<BaseSelectable> ();
					selectedParentBS.ChildrenParts.Remove (selectedPart);
					Destroy (selectedPart);

					sNewPost.SpawnChild (spawnID);

					DropdownMenu_TREX trexDropMenu = SceneManager.Instance.DropMenu as DropdownMenu_TREX;
					trexDropMenu.DeleteItemsForPart (sPost);
					trexDropMenu.CreateItemsFor (sNewPost);
					//trexDropMenu.ReorderMenuItems ();
					trexDropMenu.ResizeParent ();

					List<byte> activeChildrenTypes = sNewPost.GetActiveChildrenTypes ();
					foreach (byte partID in activeChildrenTypes)
						trexDropMenu.SetActive_ItemsForPart (sNewPost, partID);
				}
			}
			else
				Debug.LogError (((TREX_PartID)spawnID).ToString()+" does not register as a Post or Part.");
		}
		else if(sConn)
		{
			if (TREX_Connector.IsAConnector ((TREX_PartID)spawnID))
			{
				BaseSelectable selectedParentBS = sConn.ParentPart.GetComponent<BaseSelectable> ();
				List<TREX_Connection> parentConnections = selectedParentBS.Connections;

				MountingPos mountPos = MountingPos.None;
				HoleType holeType = HoleType.None;
				for(int i = 0; i < parentConnections.Count; ++i)
				{
					TREX_Connection2Conn connection = parentConnections [i] as TREX_Connection2Conn;
					if(connection.connector == sConn)
					{
						mountPos = connection.mountPos;
						holeType = connection.holeType;
						i = parentConnections.Count;
					}
				}

				GameObject newConnGO = SpawnPart (spawnID, sConn.ParentPart, mountPos);
				TREX_Connector sNewConn = newConnGO.GetComponent<TREX_Connector> ();
				SetParentChild (sConn.ParentPart, newConnGO);
				sConn.TransferOrDestroyChildren (newConnGO.GetComponent<TREX_Connector> ());

				sNewConn.Connections.Add (new TREX_Connection2Post (holeType, mountPos, selectedParentBS as TREX_Post));

				selectedParentBS.ChildrenParts.Remove (selectedPart);
				Destroy (selectedPart);

				DropdownMenu_TREX trexDropMenu = SceneManager.Instance.DropMenu as DropdownMenu_TREX;
				trexDropMenu.DeleteItemsForPart (sConn);
				trexDropMenu.CreateItemsFor (sNewConn);
				//trexDropMenu.ReorderMenuItems ();
				trexDropMenu.ResizeParent ();
				trexDropMenu.SetActive_ItemsForPart (sNewConn, (byte)sNewConn.PartID);
			}
			else
				Debug.LogError (((TREX_PartID)spawnID).ToString()+" does not register as a Connector.");
		}
	}

	protected override string GetTemplateName(byte partType)
	{
		string templateName = "INVALID";
		switch((TREX_PartID)partType)
		{
		case TREX_PartID.Part_Ab_Board:
			templateName = "TREX AB BOARD T-8300";
			break;
		case TREX_PartID.Part_Dip:
			templateName = "TREX DIP T-8000";
			break;
		case TREX_PartID.Part_MedBall_Dbl:
			templateName = "TREX DBL MED BALL TARGET T-8400";
			break;
		case TREX_PartID.Part_Step:
			templateName = "TREX STEP T-8100";
			break;
		case TREX_PartID.Part_Y_Extender:
			templateName = "TREX Y EXTENDER T-8200";
			break;
		case TREX_PartID.Part_TopCap:
			templateName = "TREX POST TOP CAP";
			break;
		case TREX_PartID.Conn_MonkeyBarBridge:
			templateName = "TREX MONKEY BAR BRIDGE T-8600";
			break;
		case TREX_PartID.Conn_MonkeyBarBridgeMount:
			templateName = "TREX MONKEY BAR BRIDGE MOUNT T-7200";
			break;
		case TREX_PartID.Conn_PullUp_Ergo:
			templateName = "TREX ERGO PULLUP T-7100";
			break;
		case TREX_PartID.Conn_PullUp_Std:
			templateName = "TREX PULLUP T-7000";
			break;
		case TREX_PartID.Post_T2200:
			templateName = "TREX POST T-2200";
			break;
		case TREX_PartID.Post_T3000:
			templateName = "TREX POST T-3000";
			break;
		case TREX_PartID.Post_T3500:
			templateName = "TREX POST T-3500";
			break;
		case TREX_PartID.Post_T4000:
			templateName = "TREX POST T-4000";
			break;
		case TREX_PartID.Post_T4600:
			templateName = "TREX POST T-4600";
			break;
		case TREX_PartID.Post_T5000:
			templateName = "TREX POST T-5000";
			break;
		case TREX_PartID.Post_T6000:
			templateName = "TREX POST T-6000";
			break;
		case TREX_PartID.Post_T6400:
			templateName = "TREX POST T-6400";
			break;
		case TREX_PartID.Post_T6500:
			templateName = "TREX POST T-6500";
			break;
		default:
			Debug.LogError (partType.ToString()+" is not recognized.");
			break;
		}
		return templateName;
	}
}
