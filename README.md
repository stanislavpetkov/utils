# utils
Some utils


#Database first
dotnet ef dbcontext scaffold "database=127.0.0.1:databox_hd.gdb;user=sysdba;password=masterkey;Dialect=3;Charset=UTF8;ServerType=0;" EntityFrameworkCore.FireBirdSql --verbose -o Models -c DBHDContext

#Gbak restore
gbak -c -v -user SYSDBA -password masterkey DATABOX_HD.FBK 127.0.0.1:databox_hd.gdb

gbak -c -v -user SYSDBA -password masterkey DATABOX_PLANETA_16x9.fbk 127.0.0.1:databox_result.gdb
