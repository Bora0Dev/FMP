# Merchant's Road: A Medieval Virtual Economy

## Research: Designing a Medieval Virtual Economy
*A Reflective Analysis of Virtual Economies and Systems Design in Mount & Blade II: Bannerlord*

### Introduction
The development of Merchant’s Road, my medieval trading simulator, has been a transformative journey that fundamentally reshaped my understanding of game design, player agency, and the intricate architecture of economic systems. When I initially conceptualized the project, my ambition was relatively modest: I sought to create a satisfying, low-friction loop in which the player travelled between distinct towns, purchased goods at low prices, and sold them for profit elsewhere. This early prototype, while functional, served primarily as a proof of concept. It demonstrated the immediate dopamine appeal of the "buy low, sell high" fantasy, but it simultaneously exposed a critical shallowness in the design. It became evident that a trading mechanic cannot exist in a vacuum; the satisfaction of trade is not derived merely from the transaction itself, but from the context in which that transaction occurs.

I became increasingly captivated by the potential of a dynamic, living economy—a system that breathes, reacts, and resists. My focus shifted from designing a game about "trading" to designing a simulation of "an economy." I wanted to build an ecosystem where markets fluctuate based on internal logic rather than scripted events, where resources become scarce due to systemic pressures, and where the player is not the center of the universe but rather a small actor adapting to a volatile environment. To achieve this, I had to move beyond simple arithmetic and engage with complex systems theory.

To build such a system, I needed a robust theoretical foundation. Relying on intuition was insufficient for creating a self-regulating economy that could withstand player exploitation. Consequently, understanding virtual economies, systemic design, emergence, and the historical realities of medieval economic structures became essential. Academic research provided the necessary frameworks to articulate why certain design decisions mattered—transforming mechanics from functional tools into expressive instruments. Simultaneously, studying Mount & Blade II: Bannerlord offered a concrete, industry-standard example of a successful medieval trading economy. It served as a masterclass in how theoretical concepts—such as supply chains, price elasticity, and risk assessment—can be applied in a commercial product.

This essay offers a comprehensive reflection on how academic debates, practical analysis, and creative experimentation shaped the development of Merchant’s Road. The Literature Review explores foundational scholarship on virtual economies, systemic game design, feedback loops, and medieval economic history. The Case Study applies these theoretical frameworks to Bannerlord, deconstructing its algorithms and explaining how its successes and limitations influenced my own design. The Reflection discusses the iterative lessons learned through development, highlighting how theory helped me interpret and refine my creative choices. The Conclusion summarises how this research supported the intellectual and practical development of my final major project.

### Literature Review

#### 1. Virtual Economies: Scarcity, Value, and Emergence
To design a convincing economy, one must first understand what gives digital objects value. Edward Castronova’s seminal work, Synthetic Worlds, provides the bedrock for this understanding. Castronova argues that virtual worlds operate as functioning economic systems where value is not arbitrary but is generated through the interplay of scarcity, labour, production, and risk (Castronova, 2005). This challenged my initial, somewhat naive assumption that game prices should be static or determined solely by a difficulty curve. Castronova’s analysis suggests that in a "synthetic" world, the economy is a membrane between the real and the virtual; the time a player invests (labour) converts into virtual capital.

If the game simply hands the player gold, the currency becomes hyper-inflated and worthless. Therefore, Merchant’s Road had to be designed as a network of interdependent relationships. The value of an item could not be an integer stored in a database; it had to be a variable derived from the distance to the source, the danger of the route, and the current scarcity of the good.

Julian Dibbell expands this perspective by exploring the sociology of value. In Play Money, he demonstrates that players construct emotional, cultural, and symbolic meaning around virtual goods (Dibbell, 2006). He posits that the "magic circle" of the game allows for the suspension of disbelief regarding value. A digital sword is just pixels, but within the context of the game’s narrative and community, it holds genuine worth. This insight fundamentally altered my approach to item design in Merchant’s Road. Initially, I viewed goods as abstract commodities—Category A, B, and C. Dibbell’s work pushed me to treat them as emotionally charged objects with narrative weight. A sack of grain is not just a trade good; during a famine event, it becomes a lifeline for a starving town. A bundle of wool represents the labor of the peasantry; spices represent the exotic and the luxury of the aristocracy. By attaching narrative context to economic assets, the act of trading becomes an act of storytelling.

Vili Lehdonvirta reinforces this by arguing that distinct constraints—scarcity, difficulty, and restricted access—are what shape perceived value (Lehdonvirta, 2010). If an item is ubiquitous, it is valueless. Goods feel meaningful only when players must overcome limitations to obtain them. This informed several critical design decisions in Merchant’s Road. I implemented a strict inventory weight limit (simulate bulk), significantly slowed travel time (simulate distance), and restricted price information (simulate the lack of communication technology). These were not "quality of life" issues to be solved; they were necessary friction. By obscuring the "perfect trade," I force the player to gamble, thereby increasing the emotional payoff when a trade is successful.

Jesper Juul provides a structural perspective in Half-Real, describing games as structures that combine formal rules with player interpretation (Juul, 2005). The meaning of a virtual economy, therefore, emerges not only from the code but from the player's mental model of that code. Juul’s distinction between the "rules" and the "fiction" helped me realize that trading systems do not need to be perfectly realistic simulations of history; they need to be consistent and interpretable systems. If the price of iron rises, the player must be able to deduce why based on the game's internal logic (e.g., "A war has started, so weapons are in demand"). The player’s ability to read the world is essential for the economy to feel fair.

Taken together, these scholars illustrate that virtual economies gain depth when value is emergent rather than assigned. This framework became the lens through which I analyzed Bannerlord and the blueprint for Merchant’s Road.

#### 2. Systems Thinking, Feedback Loops, and Emergence
Moving from the sociology of value to the mechanics of interaction, the work of Katie Salen and Eric Zimmerman became indispensable. In Rules of Play, they describe games as dynamic systems made of interconnected parts that interact to create emergent behavior (Salen and Zimmerman, 2004). Their concept of the "cybernetic feedback loop" was the structural backbone of my economy.

In a trading simulator, feedback loops are the engine of balance. A negative feedback loop stabilizes the system: if a town lacks grain, the price rises; the high price attracts merchants; merchants bring grain; the supply increases; the price drops. This self-correcting mechanism ensures the economy doesn't break. Conversely, positive feedback loops can be used to create crises: a town has no food; peasants starve; production of goods drops; the town becomes poorer; it cannot afford to buy food. Understanding these loops allowed me to design "Meaningful Play," where players understand how their specific actions (selling 500 units of grain) produce system responses (crashing the local market).

Ian Bogost introduces the critical concept of Procedural Rhetoric: the notion that games express ideas and arguments through their rules and systems, rather than their text (Bogost, 2007). This theory transformed my philosophy of design. I realized that Merchant’s Road was making an argument about medieval life. By making travel slow and inventory limited, the game procedurally argues that pre-industrial logistics were arduous. By making markets volatile, the game argues that life was uncertain. I began to view every variable—price fluctuation ranges, travel speed, bandit spawn rates—as an expressive gesture. Merchant’s Road communicates the harshness of the era through its math.

Mia Consalvo’s research on player behavior and "gaming" the system (Consalvo, 2007) provided a necessary caution. Players are efficient; they will inevitably seek to optimize the fun out of a game by finding the dominant strategy (e.g., a specific trade route that always yields profit). Consalvo’s insights on how players interpret systems through their cultural expectations helped me design against "min-maxing." I realized I needed to disrupt reliability. By designing markets that fluctuate unpredictably and introducing random events (storms, embargoes), I prevent the player from solving the economy like a puzzle. I encourage them to adapt dynamically, letting go of modern expectations of constant growth and embracing medieval precarity.

Paul Dourish focuses on embodied interaction, arguing that systems shape how users experience digital space physically and cognitively (Dourish, 2001). For Merchant’s Road, this meant designing travel that felt substantial. In many games, travel is a "loading screen" or a fast-forward button. Dourish’s work inspired me to make the movement phase the core gameplay. Moving across the map requires watching resources dwindle and scanning for threats. The "interaction" is the endurance of the journey. Travel time is not a delay; it is the cost of doing business.

Finally, James Paul Gee’s work on learning principles describes how games act as teaching machines (Gee, 2003). Players learn best through experimentation, pattern recognition, and immediate feedback. This reinforced my decision to avoid heavy-handed tutorials in Merchant’s Road. Instead, I allow the player to fail. If they buy wool in a sheep-farming town and try to sell it in another sheep-farming town, they lose money. The system provides negative feedback, teaching the player the rule: "Diversity of geography dictates trade value."

#### 3. Medieval Economic Structures and World Logic
While Merchant’s Road is a simulation, it requires the grounding of historical authenticity to sell its fantasy.

Marc Bloch’s Feudal Society portrays medieval economies as decentralized, fragmented, and intensely local (Bloch, 1961). Unlike modern globalism, medieval towns were islands of production. This helped me design settlements in Merchant’s Road with distinct, rigid identities. A mining town in the mountains has iron but no food; a valley town has grain but no metal. This interdependence is the engine of trade. Bloch’s work on the fragmentation of power also influenced the "Toll" mechanic, where passing through different territories incurs costs, representing the decentralized feudal tax system.

Georges Duby emphasizes the constant, looming presence of scarcity (Duby, 1974). The medieval economy was an "economy of survival." Crop failures, weather, conflict, and labor shortages were not anomalies; they were the norm. This informed my decision to incorporate "Events" as a core mechanic. Random supply disruptions, seasonal scarcity cycles (winter pricing vs. harvest pricing), and unpredictable spikes create an atmosphere of fragility. The player is not just trading for profit; they are often trading to keep towns alive.

Carlo Cipolla highlights the "energy constraint" of the era (Cipolla, 1980). Before the industrial revolution, all movement was limited by organic energy (muscle, wind). This justifies the distance-based pricing model in my game. Goods from faraway towns command higher prices not just because they are rare, but because the energy cost of moving them is so high. This gave me a mathematical model for calculating value: Price = BaseCost + (Distance * RiskFactor).

Fernand Braudel argues that medieval economies were shaped by the longue durée—slow-moving structural forces rather than rapid cycles (Braudel, 1982). This inspired me to design economic states that develop gradually. A town does not go from "Prosperous" to "Destitute" overnight. It is a slow decay, visible to the observant player, allowing them to anticipate market crashes before they happen.

### Case Study: Mount & Blade II: Bannerlord

#### Overview
Mount & Blade II: Bannerlord, developed by TaleWorlds Entertainment, presents one of the most sophisticated dynamic economies in modern gaming. It serves as an ideal case study because it does not "fake" its economy; it simulates it. Villages generate raw materials based on geography; villagers physically carry these goods to towns; towns consume them or refine them into luxury goods (tools, velvet, wine); workshops require inputs to function; and caravans move resources between regions to smooth out price disparities. When political instability creates war, these physical supply chains are disrupted, causing organic price inflation and scarcity.



#### Applying Academic Theories to Bannerlord
Castronova’s theory of interdependent virtual economies is clearly visible in Bannerlord’s operational logic. The economy is a closed loop. When a village is raided by a lord, its production ceases. This is not just a visual effect; the database updates. The nearby town stops receiving grain. Its food stocks deplete. The garrison begins to starve and die. The prosperity of the town drops, reducing tax income for the owner. This systemic ripple effect validates Castronova's assertion that virtual worlds function best as interconnected webs.



Lehdonvirta’s emphasis on constraints is evident in the inventory and travel mechanics. Players and AI alike are limited by "Carry Weight" and "Party Speed." A merchant cannot simply buy all the grain in the world; they are limited by the physical capacity of their mules. This constraint creates the core decision-making loop: "Do I carry high-volume/low-margin goods (grain) or low-volume/high-margin goods (jewelry)?"

Bogost’s procedural rhetoric is expressed through the game's instability. The game makes a procedural argument that war is expensive. It does not tell you this in a cutscene; it shows you by tripling the price of horses and weapons during a campaign, and by causing famine in besieged cities. The mechanics force the player to feel the economic cost of conflict.

#### Technical Analysis of Bannerlord’s Economy
To understand how to replicate this, I analyzed the specific technical layers of Bannerlord:

1. **Production and the Source**: Every settlement has a "Production" value tied to static geography. Villages are the primary producers (Primary Sector), generating raw inputs like Grain, Iron, or Flax. `Production(Daily) = BaseRate * HearthCount * SeasonalModifier`. This formula ensures that as a village grows (Hearths increase), output scales, but it remains tethered to the land.

2. **Consumption and Sinks**: A virtual economy needs "sinks" to remove items, or inflation occurs. In Bannerlord, towns act as sinks.
    *   *Passive Consumption*: The population eats food daily.
    *   *Industrial Consumption*: Workshops (Secondary Sector) consume raw materials (Olives) and delete them to spawn refined goods (Oil). This constant destruction of goods creates the vacuum that pulls trade caravans toward the town.

3. **Supply–Demand Pricing Algorithm**: Prices are not static. They update daily using a localized Supply/Demand curve. `Price = BaseValue * (Demand / Supply)^Factor`. If Supply is high, the fraction is small, and Price drops. If Supply is low (e.g., after a raid), Price skyrockets. This algorithm is the heartbeat of the game.

4. **Caravan Agents (AI)**: The game uses autonomous agents (Caravans) to balance the economy. These agents do not cheat; they scan known prices, calculate the best route, and travel physically. If they are destroyed by bandits, the goods are deleted from the world, creating genuine scarcity.

5. **Economic Shocks**: The interplay of military and economic systems is the game's brilliance.
    *   *Raids*: Halt production.
    *   *Sieges*: Halt consumption (markets close) and create extreme internal scarcity, leading to starvation.
    *   *War*: alters demand tables (armies need more grain and horses than civilians).

#### Influence of Bannerlord on Merchant’s Road
Analyzing Bannerlord provided a blueprint, but also highlighted areas I wanted to diverge from or simplify for Merchant’s Road.

*   **Fluctuating Markets**: Inspired by Bannerlord’s daily price changes, I implemented a similar volatility. However, where Bannerlord can be opaque, I added a "Rumor" system to give players hints about these fluctuations, bridging the gap between complexity and usability.
*   **Interdependent Settlements**: I adopted the "Town -> Village" relationship. In Merchant’s Road, if the player fails to bring iron to a Smithing town, the town cannot produce tools, and eventually, it cannot pay the player for other goods. The economy can "fail" if the player is inactive.
*   **Risk as a Commodity**: Bannerlord treats safety as a luxury. I adopted this. Safe routes have low profit margins (because AI caravans flood them). Dangerous routes (bandit territory) have high margins. This creates a "Risk vs. Reward" slider for the player.
*   **Slow Travel**: Bannerlord’s map movement is a distinct game phase. I replicated this, ensuring that the passage of time is the primary resource the player spends.
*   **Legibility**: Bannerlord sometimes hides its math too well. I decided to make Merchant’s Road more transparent, using visual indicators (arrows, color coding) to show price trends, ensuring the system is "readable" as per Juul’s theory.

### Reflection on Practice
Developing Merchant’s Road was not merely an exercise in coding; it was a dialogue between the theoretical concepts I studied and the practical reality of game engine constraints. This process reshaped my identity as a designer.

1.  **Systems are Expressive**: Initially, I viewed mechanics as functional interaction points. Through this project, specifically applying Bogost’s theories, I learned that mechanics are the narrative. When I adjusted the code to make food rot over time, I wasn't just adding a difficulty modifier; I was telling a story about the perishability of medieval life. The realization that I can induce feelings of panic or relief purely through a spreadsheet-based inventory system was a pivotal moment in my growth.

2.  **Constraints Produce Depth**: My research into medieval history (Cipolla, Bloch) highlighted that the medieval world was defined by what people could not do. Early in development, I gave the player infinite inventory space. The game was boring. It was only when I introduced the "Weight" constraint—forcing the player to choose between 50 heavy iron ingots or 200 light silk bundles—that the game became strategic. I learned that design is often the art of taking power away from the player to make their remaining choices matter.

3.  **Iteration is Essential**: The price algorithm was the hardest component to design. My first iteration was a global randomizer, which felt chaotic and unfair (violating Salen & Zimmerman’s "Meaningful Play"). My second iteration was too predictable. The final version, inspired by Bannerlord, uses a "Target Inventory" system: every town wants X amount of an item. The further the current stock is from X, the more the price shifts. This required weeks of tweaking variables to find the "sweet spot" where prices felt organic rather than algorithmic.

4.  **Instability Creates Engagement**: I initially feared unpredictable market shifts would frustrate players. However, testing revealed that stability is boring. When a stable route suddenly becomes unprofitable due to a "War" event, the player is forced to improvise. This aligns with Consalvo’s ideas on player adaptation. I learned that a designer’s job is to disrupt the player's equilibrium to force them to re-engage with the systems.

5.  **Player Perspective Shapes Emotion**: By locking the camera to a top-down map and restricting information (Fog of War), I aligned the player's cognitive state with that of a medieval merchant. They don't know if the road ahead is safe; they only know what they can see. This alignment of perspective and mechanic creates immersion.

6.  **Systems Must Be Readable**: A major hurdle was the UI. A complex economy generates data, and displaying that data without overwhelming the user is a challenge. Drawing on Juul’s "Half-Real," I realized the UI is the translator between the code and the player. I redesigned the trading screen three times to ensure that "Profit/Loss" and "Market Trend" were communicated instantly through color and iconography, reducing cognitive load.

7.  **Theory Enhances Creativity**: Perhaps the most significant lesson is that academic theory did not restrict my creativity; it provided the vocabulary to understand it. When a mechanic felt "off," I could look to Castronova or Lehdonvirta to understand why (usually a lack of scarcity or interdependence). Theory provided the diagnostic tools to fix design problems.

---

## Implementation: Technical Architecture and Code Analysis

### Overview
The implementation of *Merchant’s Road* required translating the high-level theoretical concepts of scarcity, interdependence, and feedback loops into concrete C# code within the Unity engine. The system is built on a modular architecture, separating the **Data Layer** (Economy Models), the **Logic Layer** (Managers), and the **Presentation Layer** (UI). This separation ensures that the economic simulation can run independently of the visual representation, allowing for background simulation and easier testing.

### 1. Telemetry and Data Collection System
To validate the design decisions discussed in the research section, a robust telemetry system was implemented. This system captures both objective performance metrics and subjective user feedback, creating a feedback loop for the developer similar to the economic feedback loops within the game.

#### The Telemetry Manager (`TelemetryManager.cs`)
The `TelemetryManager` is the core of this system. It is implemented as a **Singleton**, ensuring that one persistent instance exists throughout the game's lifecycle (`DontDestroyOnLoad`). This is crucial for tracking a session that spans multiple scenes (e.g., moving from the Main Menu to the World Map to a Town Scene).

**Key Responsibilities:**
*   **Session Initialization**: On `Awake()`, the manager generates a unique `sessionID` (GUID) and captures system hardware specifications (CPU, GPU, RAM, OS). This data is vital for contextualizing performance metrics—low FPS on a high-end machine indicates unoptimized code, whereas low FPS on a low-end machine is expected.
*   **Performance Sampling**: The `Update()` loop contains a timer that samples the framerate every second (`Time.unscaledDeltaTime`). These samples are stored in a `List<float>`. When the session ends, the system calculates the `averageFPS`. This granular data helps identify performance spikes or drops over time.
*   **Data Serialization**: The system uses Unity's `JsonUtility` to serialize the `TelemetryData` class into a JSON string. JSON was chosen for its human-readability and compatibility with external data analysis tools.
*   **File I/O**: The `SaveTelemetry()` method handles writing the data to the disk. It includes logic to sanitize filenames using Regex (`SanitizeFilename`), preventing errors if a user enters invalid characters in their name. The data is saved to `Application.persistentDataPath` for reliability, but in the Editor, it is also duplicated to a local `Logs` folder for immediate developer access.

#### User Feedback Integration (`SurveyController.cs`)
While telemetry captures *what* happened, the survey system captures *how* the player felt about it. The `SurveyController` manages the UI for user feedback.

*   **UI Binding**: The script references `TMP_InputField` and `Slider` components to capture qualitative (comments, bugs) and quantitative (1-5 rating) data.
*   **Consent Management**: Ethical data collection is enforced via a `consentToggle`. The boolean value is passed to the `TelemetryManager`, ensuring that no data is processed without explicit user permission.
*   **Data Flow**: When the user clicks "Submit", the controller extracts values from the UI elements and passes them to `TelemetryManager.Instance.SubmitFeedback()`. This decoupling means the UI doesn't need to know *how* the data is saved, only *where* to send it.

### 2. The Market System and UI Interaction
The economic simulation is visualized through the `TownMarketUI` class. This script serves as the bridge between the underlying `PriceDatabase` (the simulation) and the player (the interaction).

#### Market Interface (`TownMarketUI.cs`)
The market UI is designed to be "readable," addressing the issue of legibility raised in the research section.

*   **Dynamic Slot Population**: The `UpdatePrices(TownId town)` method iterates through the `GoodType` enum. For each good, it queries the `GameManager.instance.priceDatabase` to get the current local price. This price is then injected into a `MarketItemSlot`. This dynamic generation allows the game to support any number of goods without requiring manual UI updates.
*   **State Management**: The class manages the state of the UI, including opening/closing panels and handling sub-dialogs like the `TradeDialog`.
*   **Interaction Blocking**: To prevent player errors and maintain focus, the `DisableOtherButtons()` method uses `FindObjectsByType<Button>` to globally disable all other interactable elements when the market is open. This ensures the player cannot accidentally navigate away or trigger other events while in the middle of a transaction. It stores the original state of these buttons in a dictionary (`_buttonStates`) to restore them accurately when the market closes.

#### Trade Logic (`TradeDialog.cs`)
The actual transaction logic is encapsulated in the `TradeDialog`. This component handles the "Buy" and "Sell" operations. It validates transactions by checking:
1.  **Player Gold**: Can the player afford the item?
2.  **Inventory Space**: Does the player have enough weight capacity? (Enforcing the "Constraints Produce Depth" principle).
3.  **Town Inventory**: Does the town have the item in stock?

### 3. Systemic Design Patterns
The codebase relies heavily on **Manager Classes** and **Singletons** (`GameManager`, `TelemetryManager`) to maintain global state. This is essential for an economy simulation, as the state of the world (prices, town inventories) must persist independently of the player's location.

The use of **ScriptableObjects** (implied by the `GoodType` and database structure) allows for data-driven design. New items can be added to the economy by simply creating a new data asset, without rewriting code. This extensibility is key for future iterations.

---

### Next Steps

The current iteration of *Merchant’s Road* successfully implements the core loop of travel, trade, and economic fluctuation. However, to fully realize the vision of a "living economy," several key features are planned for the next development cycle:

1.  **AI Competitors**: Currently, the economy reacts to the player and abstract system rules. The next step is to introduce AI caravans that physically travel the map. These agents will buy low and sell high, competing directly with the player for resources. This will introduce the "Interdependence" discussed by Castronova on a more granular level.
2.  **Dynamic Event System**: While random events exist, they are currently simple text notifications. Future updates will visualize these events—e.g., a "Famine" event visually changing the town sprite to look dilapidated, or a "War" event spawning visible armies on the map.
3.  **Advanced Telemetry Visualization**: The current telemetry is saved as raw JSON. A future tool will be built to parse these logs and generate heatmaps of player death locations and trade routes, providing visual data to balance the map design.
4.  **Save/Load System**: Implementing a robust serialization system for the entire game state (not just telemetry) to allow for longer play sessions.
5.  **Cloud Persistence with Supabase**: Migrating player progression tracking to a Supabase database. This will enable cross-device saves, global leaderboards, and persistent world states, moving beyond local JSON serialization.

### Bibliography

*   Bloch, M. (1961) *Feudal Society*. London: Routledge.
*   Bogost, I. (2007) *Persuasive Games: The Expressive Power of Videogames*. Cambridge, MA: MIT Press.
*   Braudel, F. (1982) *Civilization and Capitalism, 15th-18th Century, Vol. I: The Structures of Everyday Life*. New York: Harper & Row.
*   Castronova, E. (2005) *Synthetic Worlds: The Business and Culture of Online Games*. Chicago: University of Chicago Press.
*   Cipolla, C. M. (1980) *Before the Industrial Revolution: European Society and Economy, 1000-1700*. London: Methuen.
*   Consalvo, M. (2007) *Cheating: Gaining Advantage in Videogames*. Cambridge, MA: MIT Press.
*   Dibbell, J. (2006) *Play Money: Or, How I Quit My Day Job and Made Millions Trading Virtual Loot*. New York: Basic Books.
*   Dourish, P. (2001) *Where the Action Is: The Foundations of Embodied Interaction*. Cambridge, MA: MIT Press.
*   Duby, G. (1974) *The Early Growth of the European Economy: Warriors and Peasants from the Seventh to the Twelfth Century*. Ithaca: Cornell University Press.
*   Gee, J. P. (2003) *What Video Games Have to Teach Us About Learning and Literacy*. New York: Palgrave Macmillan.
*   Juul, J. (2005) *Half-Real: Video Games between Real Rules and Fictional Worlds*. Cambridge, MA: MIT Press.
*   Lehdonvirta, V. (2010) ‘Virtual Economies: Theory and Practice’, in *Sociological Research Online*.
*   Salen, K. and Zimmerman, E. (2004) *Rules of Play: Game Design Fundamentals*. Cambridge, MA: MIT Press.
*   TaleWorlds Entertainment (2020) *Mount & Blade II: Bannerlord* [Video Game]. Ankara: TaleWorlds Entertainment.
