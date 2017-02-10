
public enum MountingPos : byte
{
	None,
	North,
	South,
	East,
	West
}

public enum HoleType : byte
{
	None,
	Low,
	High
}

public enum TREX_PartID : byte
{
	// Keep Parts, Posts and Connectors together in sequential order. Parts first, Posts next, Connectors last.
	// in other parts of the program we will check if a TREX_PartID is within a range to determine if it is a Part, Post or Connector.
	None = 0,
	Part_Ab_Board = 1,
	Part_Dip = 2,
	Part_MedBall_Dbl = 3,
	Part_Step = 4,
	Part_Y_Extender = 5,
	Part_TopCap = 6,
	Post_T2200 = 7,
	Post_T3000 = 8,
	Post_T3500 = 9,
	Post_T4000 = 10,
	Post_T4600 = 11,
	Post_T5000 = 12,
	Post_T6000 = 13,
	Post_T6400 = 14,
	Post_T6500 = 15,
	Conn_MonkeyBarBridge = 16,
	Conn_MonkeyBarBridgeMount = 17,
	Conn_PullUp_Std = 18,
	Conn_PullUp_Ergo = 19
}

public enum NOVA_PartID : byte
{
	None,
	NFTS_1000,
	Conn_StdPullUp,
	Conn_BasicCrossmember,
	Conn_LogoCrossmember,
	Conn_ArchBridge,
	Conn_HorizontalBridge_Long,
	Conn_HorizontalBridge_Short,
	Part_AccessoryLoop,
	Part_GRT_Single,
	Part_GRT_Double,
	Part_Dip,
	Part_Step,
	Part_RopeAnchor,
	Part_SlidingRopeAnchor,
	Part_RopeSlide_Single,
	Part_RopeSlide_Double,
	Part_KickPlate,
	Part_StorageTrays,
	Part_StallBar,
	Part_SquatStand,
	Part_GLoops,
	Part_SlidingPullUp,
	Part_ErgoPullUp,
	Part_ClimberBar,
	Part_ConnBracket_N6,
	Part_ConnBracket_N4_Left,
	Part_ConnBracket_N4_Right,
	Part_SideRailPullUp,
	Part_GlobeGrips
}

public enum FrameType : byte
{
	None,
	TREX_4Post,
	TREX_5Post,
	TREX_7Post,
	TREX_10Post,
	TREX_12Post,
	NOVA_1,
	NOVA_4,
	NOVA_6
}

public enum LabelsSetting : byte
{
	AllOff,
	AllOn,
	MouseOverOn
}

public enum SubConfigType : byte
{
	None,
	Single,
	Extended,
	Super,
	OnePost,
	TwoPost,
	ThreePost
}
