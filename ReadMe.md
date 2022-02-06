

## **Snacks Vending Machine**

**Description**:

Simple C# Console project made with simple oop design with some unit test.

  

**Code Skelton**:

    // Main functionality of this method is to control the flow
    main(){
    
    if (insertingCoin){
    snackMachine.HandleCoin();
    }
    if(insertingCard){
    snackMachine.HandleCard();
    }
    if(insertingNote){
    snackMachine.HandleNotes();
    }
    }
    
    // Main functionality of this class is to handle inserting and validate money.
    SnackMachineClass extends ISnackMachineInterface{
    
    handleCoins();
    
    handleNotes();
    
    handleCards();
    
    validateMoney();
    
    displayItems();
    
    returnChange();
    
    }
    
      

**Running This app:**

1. clone it by running  git clone `https://github.com/AhmadHddad/SnackMachine.git`

2. assuming you have the latest dotnet 6 installed on your machine, run `dotnet build.`

3. when building is done, run `dotnet run`;

  

The following GIF shows use cases:

![How To Use Gif](https://github.com/AhmadHddad/SnackMachine/blob/main/assets/runningApp.gif?raw=true)

**Testing**:
To test the app run `dotnet test` you can add more test units, on Test class,
the following GIF shows Testing results.

![How To Test Gif](https://github.com/AhmadHddad/SnackMachine/blob/main/assets/testingApp.gif?raw=true)
