-> main

=== main ===
My name's Kevin. Complete tasks - get money, it's simple. #speaker:Kevin
    + [See you soon]
        -> chosenBye
    + [Show available tasks]
        -> chosenStory
=== chosenBye ===
See you later. Don't forget to complete the tasks.
-> END
=== chosenStory ===
Here you are.
#show:showTasks
-> END