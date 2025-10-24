"""
Example usage of the Dice Game Analyser
"""

from Analyser import DiceGameAnalyser

def main():
    # Create an instance of the analyser
    analyser = DiceGameAnalyser("data.csv")
    
    # Run the complete analysis
    analyser.run_complete_analysis()
    
    # Make some custom predictions
    print("\n" + "="*60)
    print("CUSTOM PREDICTION EXAMPLES")
    print("="*60)
    
    # Example 1: Low stakes, high rerolls, with killshot and panic
    print("\nScenario 1: Low stakes, high rerolls, with killshot and panic")
    analyser.predict_decision(odds=0, stakes=1, rerolls=3, killshot=True, panic=True)
    
    # Example 2: High stakes, low rerolls, no killshot but with panic
    print("\nScenario 2: High stakes, low rerolls, no killshot but with panic")
    analyser.predict_decision(odds=0, stakes=5, rerolls=1, killshot=False, panic=True)
    
    # Example 3: Medium stakes, medium rerolls, with killshot but no panic
    print("\nScenario 3: Medium stakes, medium rerolls, with killshot but no panic")
    analyser.predict_decision(odds=0, stakes=3, rerolls=2, killshot=True, panic=False)
    
    # Example 4: Very high stakes, low rerolls, no killshot, with panic
    print("\nScenario 4: Very high stakes, low rerolls, no killshot, with panic")
    analyser.predict_decision(odds=0, stakes=8, rerolls=1, killshot=False, panic=True)

if __name__ == "__main__":
    main()
