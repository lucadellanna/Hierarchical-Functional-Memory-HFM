# Hierarchical Functional Memory (HFM)
A framework for an AI able to instinctively disambiguate contexts, built upon Numenta's Hierarchical Temporal Memory (HTM) Framework.

This is an implementation of the whitepaper "Techniques for the Emergence of Meaning in Machine Learning" https://www.luca-dellanna.com/wp-content/uploads/2019/01/Techniques-for-the-Emergence-of-Meaning-in-ML.pdf; the invention highlighted in the whitepaper is patent pending.

I implemented the code from Numenta's HTM 2011 paper and from the 2019 paper linked above **with a focus on distributed computation on a neural level**. (I strongly believe that, in order to represent how complex systems behave, bottom-up computation is necessary.)

In particular, I did not create objects such as columns to contain neurons, in order to allow for plasticity. Instead, each neuron is assigned coordinates in a three-dimensional space (x, y, z). Neurons sharing the same x and y are in the same column.

(work in progress, I plan to finish the readme on the evening of the 27th of March, 2019)
