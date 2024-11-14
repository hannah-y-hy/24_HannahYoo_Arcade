Code for the flowchart(quiz) from 11/07 class 
Flowchart image link from F24 gmae design discord: https://cdn.discordapp.com/attachments/1274856263645270216/1304169566406053900/20241107_144321.jpg?ex=6736fbce&is=6735aa4e&hm=f8a29d97cf5fe51f1e861869353744c3e7d0c47ed80d51cf95ee69e953d260d2&

1. When a bee stings Charlie, how would you code that?
    a. Define the sting happening:
        void StingCharlie(GameObject bee)
    {
        int stings = bee.GetComponent<Bee>().Stings; // Get sting value (1 or 2)
        stingNumber += stings; // Increase sting count for Charlie
        ChangeCharacterSprite();
        Destroy(bee); // Remove bee after stinging
    }

    b. Change a Sprite on the character GameObject:
        void ChangeCharacterSprite()
    {
        // Change character sprite to show Charlie was stung
        characterGO.GetComponent<SpriteRenderer>().sprite = stungSprite;
    }
    c. Change the Sting number:
        stingNumber += bee.GetComponent<Bee>().Stings;
        //This would make sting count increase by the value of the bee (1 or 2 because some bees are more dangerous than the other bees, as mentioned in question #3.)

2. If you always want exactly 3 bees in the scene at all times, how would you code that?
//This would keep track of the current bee count (currentBeeCount). Whenever it is less than 3, a new bee is spawned.
void Update()
{
    if (currentBeeCount < 3)
    {
        SpawnBee();
    }
}

void SpawnBee()
{
    Vector2 randomPosition = GetRandomPosition();
    Instantiate(beePrefab, randomPosition, Quaternion.identity);
    currentBeeCount++;
}

3. Some bees are more dangerous: their stings count as 2 stings.
    
a. Define the dangerous bee:
    public class Bee : MonoBehaviour
{
    public int Stings; // 1 for normal bee, 2 for dangerous bee
}

b. Increase sting number by 2 if dangerous bee
//This handles both dangerous and normal bees based on the Stings value.
stingNumber += bee.GetComponent<Bee>().Stings;

4. How will you code the win state?
//This checks if there are no bees left and if the player has clicked & removed all target bees, then triggers the win state.
void CheckWinState()
{
    if (currentBeeCount == 0 && totalBeesRemoved == targetBeeCount)
    {
        Debug.Log("You Win!");
        ShowWinUI();
    }
}

5. How will you code the lose state?
//If stingNumber reaches or exceeds 5, the game transitions to the lose state.
void CheckLoseState()
{
    if (stingNumber >= 5)
    {
        Debug.Log("You Lose!");
        ShowLoseUI();
    }
}


<Code architecture you should start with>
1. LogicGO
 int stingNumber; :This Tracks the number of stings Charlie has received.

2. characterGO
 This contains ChangeCharacterSprite() for changing the sprite when Charlie is stung.

3. BeePrefab
int Stings: This determines the sting severity (1 or 2) for each bee type(normal/dangerous).