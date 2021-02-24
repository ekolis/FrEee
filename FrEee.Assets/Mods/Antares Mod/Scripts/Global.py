# Mod global script code goes here.
# This script will be imported into all mod scripts, including formulas.

# Reduces the size of a component to half at max level.
def miniaturize(size, level):
	return interpolate(level, 0, 5, size, size / 2);

# Reduces the cost of a component to one quarter at max level.
def discount(cost, level):
	return interpolate(level, 0, 5, cost, cost / 4);

# Linearly interpolates a Y value based on an X value and ranges for X and Y.
def interpolate(x, x1, x2, y1, y2):
	ratio = (x - x1) / (x2 - x1);
	return y1 + ratio * (y2 - y1);