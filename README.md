# fixMatlabEPS
Fixes the artefacts seen on MATLAB EPS exports in versions R2014b and higher.

Since the change to the vector graphics renderer in MATLAB R2014b, the new way in which elements are grouped together leads to a number of white lines appearing between regions, which are especially clear on some contour plots. This issue has been well-reported (see, for example, [here](https://uk.mathworks.com/matlabcentral/answers/605456-print-eps-or-pdf-contourf-plot-with-edgecolor-none-leaves-white-lines-between-levels-need-to), [here](https://uk.mathworks.com/matlabcentral/answers/166366-white-line-etching-on-exported-eps-figures-in-r2014b) and [here](https://github.com/altmany/export_fig/issues/44) but is, as yet, unfixed - it appears earlier attempts such as [this](https://github.com/Sbte/fix_matlab_eps) from [Sbte](https://github.com/Sbte/) do not fix all cases.

A solution is proposed which involves modifying the EPS file [here](https://uk.mathworks.com/matlabcentral/answers/166366-white-line-etching-on-exported-eps-figures-in-r2014b) but it is noted that this has the undesired effect of making the text bold. This tool modifies the fill command but seeks out any grayscale elements (including text) and doesn't apply the fix to them; it's crude, but it works for many simple cases.

## Usage
At the command-line, run
`fixMatlabEPS "(filename here)"`

A `.m` file function for saving EPS files is also provided, giving the function `saveeps(filename)` in case this is easier. The function needs to be in the same folder as `fixMatlabEPS.exe`.


