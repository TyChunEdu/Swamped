using System.Linq;

namespace Items
{
    public class RobotsWithQuantity : PseudoDictionary<Robot, int>
    {        
        public int NumberOfRobot(Robot robot)
        {
            int val;
            AsDictionary().TryGetValue(robot, out val);
            return val;
        }
    
        public void SetRobotQuantity(Robot robot, int quantity)
        {
            var newDict = AsDictionary();
            newDict[robot] = quantity;
            FromDictionary(newDict);
        }
        
        public void AddRobot(Robot robot, int quantity = 1)
        {
            SetRobotQuantity(robot, NumberOfRobot(robot) + quantity);
        }

        public int TotalQuantity()
        {
            return AsDictionary().Values.Sum();
        }

        /// <summary>
        /// Casts to ItemsWithQuantity
        /// </summary>
        public static implicit operator ItemsWithQuantity(RobotsWithQuantity robotsWithQuantity)
        {
            var itemsWithQuantity = new ItemsWithQuantity();
            foreach (var (robot, quantity) in robotsWithQuantity.AsDictionary())
                itemsWithQuantity.AddItem(robot, quantity);
            return itemsWithQuantity;
        }
    }
}