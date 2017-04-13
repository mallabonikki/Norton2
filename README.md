## Norton2 Windows Service App
  #### A Timer to check a file for modification.
  *  Included debuging mode and release mode.
  *  Note on deployment:
     *  Always get x86/release/norton2.exe and copy to Norton2/ folder.
     *  Do not run just rebuild it.
  *  Have separate ServiceWork class to check the file for changes and shutdown it immidiately.
     *  Timer was set to 70Sec, enough to check the file every minute.
