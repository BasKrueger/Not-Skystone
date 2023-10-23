public class SkystoneModel 
{
    public int northSpikes { get; private set; }
    public int eastSpikes { get; private set; }
    public int southSpikes { get; private set; }
    public int westSpikes { get; private set; }

    public int ownerIndex {private get; set; }

    public SkystoneState GetState()
    {
        return new SkystoneState
        {
            northSpikes = this.northSpikes,
            eastSpikes = this.eastSpikes,
            southSpikes = this.southSpikes,
            westSpikes = this.westSpikes,

            OwnerIndex = this.ownerIndex
        };
    }

    public bool TryToOvertake(SkystoneModel other)
    {
        if(other.ownerIndex != this.ownerIndex)
        {
            other.ownerIndex = this.ownerIndex;
            return true;
        }
        return false;
    }

    public void SetSpikes(int north, int east, int south, int west)
    {
        northSpikes = north;
        eastSpikes = east;
        southSpikes = south;
        westSpikes = west;
    }
}
