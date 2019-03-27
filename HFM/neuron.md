# Neuron

I implemented the code from Numenta's HTM 2011 paper and from my 2019 paper **with a focus on distributed computation on a neural level**.

The neuron's output state is updated only once per iteration throught the SetFiringStatus method, called at the end of Brain.ProcessNewStimulus().

The neuron's output state is computed by checking whether enough feedforward input is present (i.e., IsExcited) and whether it is not inhibited.

A neuron is inhibited if it is inhibited either by other columns or by other neurons in the same column.
A neuron is inhibited by other columns if other columns nearby have a stonger feedforward input.
A neuron is inhibited by other neurons in the same column if other neurons are preactivated (aka in prediction mode) and the neuron being considered isn't preactivated itself.
