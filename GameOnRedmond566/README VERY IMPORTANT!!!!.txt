Before more dev work, please revert back to QA environment (change bittoys object to qa server, and auth key to original, found in slack channel by searching "keystore"

Also for feedback to improve the project-check the slack channels

(obviously the analytics and metrics needs some work :))

Also, this was noted in the other txt doc, but it is important to NOT copy and paste from the bittoys samples as the original author did a bad job and put unprintable chars which results in weird compiler errors

J [2:15 PM]
@Shawn T @Tommy Hanusa please revert your code back to the `qa` environment.  This means a change to the Bit Toys plugin and inserting the old `authKey`.  keep the `prod` `authKey` in a safe place, of course.  Right now your access to `prod` is temporarily suspended as we provision the servers back to pre-event loads.  Thanks


Shawn T [2:16 PM]
ok

J [2:18 PM]
please also note that `styleId` = `dragon_run_2018`, the 1,000 NFC cards that we gave out at the event, will not work with your app when it’s against `qa` environment.  please use `dragon_run_2018_dev`
and for now, we will allow `dragon_run_2018_dev` to be active on `prod` for a few more days