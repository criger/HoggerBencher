# HoggerBencher

## What is HoggerBencher
HoggerBencher is a C# program, that is able to show you the difference in both CPU and RAM performance when it comes to the following different classes:<br />

* String VS StringBuilder
* Array VS List VS Dictionary
  - In this particular test you can choose between inserting Strings or Integers
  - This test also shows the difference in performance between Strings and Integers as well :-)

All tests measure CPU performance as ___TIME SPENT ON TEST___.

* In addition it also has a fun brainteezer test, that can be used to see that something that may seem obvious, not necessarily is obvious 😹

## How about Java? Can CPU/RAM performance be measured there? 

Short answer: Yes<br />
Long answer:<br />
CPU performance is not a problem, but as for RAM performance, it's not possible without a lot of extra hassle. This has to do with the way JVM works on top of the runnning OS.<br /><br />
Extraxting RAM performance data from Java's JVM Garbage Collector is not a straight forward operation (still working on it...), but at least this was far more easier with C# due to .NET being a part of Windows. 

## Do you have a Java solution for this?
YES<br />
But I'll wait on releasing that version since it has some problems in terms of measuring correct RAM usage.

## How accurate is the extracted data? 
Very accurate, however... If you see some strange data, try running the test again. <br />
This happens when NET Garbage Collector suddenly runs in the middle of the benchmark  Even if it is instructed to wait until the test is over. <br />
This is by design from Microsoft and is nothing anybody can do anything about. It happens when the system finds out that it needs to free up RAM due to it being needed somewhere else.

## How much memory is "saved" from Garbage Collector prior to running the tests?
Upon starting the tests, a quarter of available memory is "saved" from being reset by GC.<br/>
E.g:<br/>
You have 10 GB RAM available of 16 GB total.<br/>
In this case, 2.5 GB of RAM will be marked "safe" to be used by the program.
Immediately after the test is finished, <br/>GC ___is instructed to run___.
