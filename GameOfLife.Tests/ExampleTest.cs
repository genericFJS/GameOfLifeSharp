namespace GameOfLife.Tests
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void Example_NextUniverseIsCorrect()
        {
            // ARRANGE
            // Ausgangs-Universum
            //   0 1 2 3 4
            // 0
            // 1     x
            // 2       x
            // 3         x
            // 4     x
            var universe = new Universe();
            universe.VitalizeCell(2, 1);
            universe.VitalizeCell(3, 2);
            universe.VitalizeCell(4, 3);
            universe.VitalizeCell(2, 4);

            // ACT
            var nextUniverse = universe.Next();

            // ASSERT
            // Ziel-Universum
            //   0 1 2 3 4
            // 0
            // 1     
            // 2       x
            // 3       x    
            // 4     
            var expectedUniverse = new Universe();
            expectedUniverse.VitalizeCell(3, 2);
            expectedUniverse.VitalizeCell(3, 3);
            Assert.IsTrue(nextUniverse.AliveCells.SetEquals(expectedUniverse.AliveCells));
        }
    }
}
