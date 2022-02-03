function saveeps(filename)
    print('-depsc2', filename, '-painters');
    system(strcat("C:\matlab_utils\fix_eps.exe """, filename, """"));
end

