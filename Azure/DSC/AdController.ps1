configuration AdController 
{
    Node localhost 
    {

        File ADFiles 
        {
            DestinationPath = "c:\NTDS"
            Type = "Directory"
            Ensure = "Present"
        }

        WindowsFeature ADDSInstall 
        {
            Name = "AD-Domain-Services"
            Ensure = "Present"
        }

        WindowsFeature ADDSTools 
        {
            Name = "RSAT-ADDS"
            Ensure = "Present"
        }
    }
}