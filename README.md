# Tirais Unity Library (Beta)
Library that extends the functionality of Unity and simplifies the development of 2D and not only games.

# Main Features
* Custom Ticker with changeable logic execution speed, regardless of global **Unity** time
* State Machine for realization "State" pattern
* Custom Animator State Machine for flexible control of animation transtions
* Generic Database based on dictionaries for storage various objects. In particular has been designed to simplify the handling of Addressables assets and centralized acces to them through the registry and groups system
* Input handler for Input System Package, which incapsulates logic of subscribing and unscribing to **InputAction** events
* Character 2D move controller
* Inventory System based on pattern **MVVM** with **UniRX** reactive properties
* Diagnostic messages like **Exception**, but specialized for **Unity** "Debug"
* Collections: **BinaryHeap**, **MinHeap**, **MaxHeap**, **PriorityQueue**
* Utilites with custom attributes, for manual initialization **Unity** objects and manage initilaztion order, also supports simple dependency injection system

# Install
* Download and install UniRX 3
* Download archive and put in **Unity** project assets folder

# In Future
* Make the library fully modular
