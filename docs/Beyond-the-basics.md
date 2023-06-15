# Beyond the basics

We have covered the basics of the validations [here](../README.md).
To enhance/simplify for many implementations, I have written a small extension `HCValidation` to avoid duplication of code. I.e. created an API for working with validations.

This requires to build the library and some modification to the web application to get it to work. (I will not describe this here now, but it can be figured out by a handy developer by the git commits)

This will lead to that we can simplify the method [HC_PartGetValidations](../src/packages/hc_core_examples/Import/Method/HC_PartGetValidations.xml) by replacing it with the equivalent [HC_PartGetValidations2](../src/packages/hc_core_examples/Import/Method/HC_PartGetValidations2.xml)
