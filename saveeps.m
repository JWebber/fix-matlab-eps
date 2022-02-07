function saveeps(filename)
    print('-depsc2', filename, '-painters');
    fclose('all'); % Prevent IO exceptions
    system(strcat("PATH_TO_FIXMATLABEPS.EXE HERE """, filename, """"));
end

