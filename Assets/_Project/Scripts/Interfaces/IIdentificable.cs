
public enum ObjectID
{
    PLUSHIE_BEAR = 0,
    PLUSHIE_BUNNY = 1,
    PLUSHIE_COW = 2,

    CHESS_W_PAWN = 15,
    CHESS_W_KNIGHT = 16,
    CHESS_W_ROOK = 17,
    CHESS_W_KING = 18,
    CHESS_W_QUEEN = 19,
    CHESS_W_BISHOP = 20,

    CHESS_B_PAWN = 21,
    CHESS_B_KNIGHT = 22,
    CHESS_B_ROOK = 23,
    CHESS_B_KING = 24,
    CHESS_B_QUEEN = 25,
    CHESS_B_BISHOP = 26,

    SO_GRANADE_NOISE = 50,
    SO_GRANADE_STUN = 51,

    NONE = 100,
}

public interface IIdentificable
{
    public ObjectID ID { get;}
}
