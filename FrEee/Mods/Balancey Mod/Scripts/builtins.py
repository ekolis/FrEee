# Mod global script code goes here.
# This script will be imported into all mod scripts, including formulas.

def LevelUp(base, perLevel):
	'''Given a base value and a per-level modifier, calculate the value of a field from the level parameter.
	Base value is the value at level 1.'''
	return base + perLevel * (level - 1);
	
def LevelUpRanged(base, perLevel, perRange):
	'''Same as LevelUp but it also takes into account the range at which a weapon is being fired.
	Base damage is inflicted at level 1 and range 0.'''
	return LevelUp(base, perLevel) + perRange * range;