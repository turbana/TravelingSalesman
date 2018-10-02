# Traveling Salesman
An implementation of the [Traveling Salesman problem](https://en.wikipedia.org/wiki/Travelling_salesman_problem) written in C# using [Simulated Annealing](https://en.wikipedia.org/wiki/Simulated_annealing) as a search method. Results are displayed in a Windows Form while the search is performed in a separate thread. This was written primarily as learning exercise for both C# and Visual Studio.

## Simulated Annealing
Simulated Annealing is a stochastic hill climbing algorithm. It's similar to a greedy hill climb, in that it will always move to a better solution, but differs in that it sometimes moves to a worse solution. This allows the algorithm to escape from local maxima's where a greedy version would be stuck.

### Overview
SA has the benefit of being very simple, it can be generalized as:

1. Look at a neighboring state.
2. If it's better than the current state, move to it.
3. If it's worse, maybe move to it; with the odds decreasing the longer we run the algorithm.
4. Repeat 1-3 until we find a good solution or the odds of moving away have reached a minimum.

#### Pseudocode
```
state <- random state
heat <- 1.0
while heat > 0
  neighbor = nearby state
  if neighbor.score < state.score
    state <- neighbor
  else if random() < e^((state.score - neighbor.score) / heat)
    state <- neighbor
  if state.score < best.score
    best <- state
  heat *= 0.9
return best
```

### Components
SA has only two requirements that need to be configured for a specific solution: a scoring function, and a neighboring function.

#### Scoring Function
This function should return a value representing how good the current solution is. SA uses this to determine which of two states are better. As this is used multiple times in the inner loop it should be reasonably fast.

#### Neighbor Function
This should return a new state that is a "neighbor" to the current state. SA works best when we make small changes to the state, this allows us to "cool" similar to how metal atoms crystallize during cooling. So it's best if this function makes a single change to the state. For the traveling salesman we simply swap the order of visiting two cities.

#### Acceptance Function
We need to decide how often we move to a worse state, that's where the acceptance function comes in. It's typically written as `e ^ ((current_score - neighbor_score) / heat)`. This has two important properties: it decreases as the temperature cools and it decreases as the neighboring state worsens. So we are less likely to move to much worse states and we're less likely to move states as the temperature falls.

### Configuration
There are a variety of ways to tweak SA to a specific problem, here are a few:

#### Initial Heat / Minimum Heat
Maybe we want to start at a cooler temperature or end at a higher temperature.

#### Cooling Rate
The rate at which the temperature drops can be configured, this is also known as a "cooling schedule".

#### Settling
In metallurgy, metal is cooled slowly to give a chance for atoms to align into a crystalline structure. We can simulate that by running the inner loop multiple times after each cooling. This gives us a chance to explore the search space at a given temperature before moving on.

#### Random Restart
Every so often we might choose to restart the algorithm. This can help us escape a local maxima. We might restart at the best known state or even move to a random state.

#### Multiple threads
We can easily start multiple threads to parallelize the search. One option is to have two threads that cool at different rates. One cools quickly, the other slowly and every so often swap between them.