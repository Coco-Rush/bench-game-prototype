# bench-game-prototype
Epic ultra mega omegalul prototype
## TODO:
When it comes to implementing the functionality of building sentences with predefined words, let me suggest the following:
- Word : ScriptableObject
Since we work in unity, with scriptableobjects i can easily create multiple assets, which exist outside the actual in-game build.
There these can be easily imported through a json file.
- WordBehaviour : MonoBehaviour
This is the actual Behaviour class of the words. This already exists. However, i wanna change this to an abstract class.
Abstract class means that i cannot create an object/reference of this class.
- IConjunctionable interface
This interfaces are for major word classes like verbs and adjective where the actual word can change depending on the sentence or choice.
- Verb, Noun, Adjective, Pronoun, Determiner, Conjunction : Word
These are all classes that inherit from the Word abstract class. There we wanna define some specific unique code that should run depending on the classes.
  (For example for Verbs when they get dropped in the sentence, it should conjugate based on the pronoun -> iconjunctionable interface)

Good? Bad?
