public class GridCoordinate {
    private int x, z;

    public GridCoordinate(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public bool Equals(GridCoordinate other) {
        return this.x == other.x && this.z == other.z;
    }

    public void Offset(EventAction action) {
        switch(action) {
            case EventAction.Up:
                this.z += 1;
                break;
            case EventAction.Down:
                this.z -= 1;
                break;
            case EventAction.Right:
                this.x += 1;
                break;
            case EventAction.Left:
                this.x -= 1;
                break;
        }
    }

    public int GetX() {
        return x;
    }

    public int GetZ() {
        return z;
    }
    
    public GridCoordinate Clone() {
        return new GridCoordinate(x, z);
    }
}