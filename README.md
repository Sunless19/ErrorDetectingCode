# Two-Dimensional Parity Bits
## Input:
- A string of binary characters with a length that is a multiple of 7.
- (Input validation will be performed to ensure this requirement.)
## Process:

- The message will be arranged in a matrix with 7 columns and a variable number of rows (depending on the length of the input message).
- Parity bits will be calculated for each row and each column as follows:
  - For an even number of 1s in a row/column, a 0 will be added in the last column/row.
  - For an odd number of 1s, a 1 will be added.
  - The most significant bit will be the bit in the bottom-right corner of the matrix.
## Output:

- The resulting matrix will be displayed.
## Error Simulation:

- The message corruption will be simulated by randomly selecting a position in the message and modifying the bit at that position.
## Error Detection and Correction:

- The destination will recalculate the two-dimensional parity bits and identify the corrupted bit.
## Output:

- The destination will then display the corrupted position.

# CRC (Cyclic Redundancy Check)
## Input:

- A string of binary characters and a generator polynomial (with coefficients 0 and 1).
## Validation:

- Checks will be performed to ensure that both strings are binary and the length of the message is greater than the number of coefficients in the generator polynomial.
## Process:

- The message will be extended with a number of zeros equal to the degree of the entered polynomial.
- Successive XOR operations will be performed between the extended message and the polynomial coefficients until the length of the resulting remainder is strictly less than the length of the coefficient string.
Intermediate Results:

## Intermediate results of the XOR operation will be displayed.
## Final Operation:

- The XOR operation will be repeated between the extended message and the final remainder obtained, positioning the remainder below the end of the extended message.
## Output:

- The resulting CRC check value will be displayed.
